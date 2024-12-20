using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class PrototypeHeroDemo : MonoBehaviour
{

    [Header("Variables")]
    public SpiritState m_State = SpiritState.Physical;
    [SerializeField] private float m_maxSpeed = 4.5f;
    [SerializeField] private float m_jumpForce = 7.5f;
    [SerializeField] private float jumpMaxTime = 2f;
    [SerializeField][Range(0.01f, 10)] private float jumpStartPower = 2f;
    [SerializeField] private float jumpTimer;
    [SerializeField] private bool m_hideSword = false;
    [Header("Effects")]
    [SerializeField] private GameObject m_RunStopDust;
    [SerializeField] private GameObject m_JumpDust;
    [SerializeField] private GameObject m_LandingDust;
    [Header("AudioClips")]
    [SerializeField] private AudioClip[] m_RunSounds;
    [SerializeField] private AudioClip m_JumpSound;
    [SerializeField] private AudioClip m_LandSound;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Prototype m_groundSensor;
    private AudioSource m_audioSource;
    private AudioManager_PrototypeHero m_audioManager;
    private bool m_grounded = false;
    private bool m_moving = false;
    private int m_facingDirection = 1;
    private float m_disableMovementTimer = 0.0f;


    public static event Action Spritualize;
    public static event Action DeSpritualize;

    public enum SpiritState
    {
        Physical, Spiritual
    }

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();
        m_audioManager = AudioManager_PrototypeHero.instance;
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Prototype>();
    }

    // Update is called once per frame
    void Update()
    {
        Spiritualize();
        // Decrease timer that disables input movement. Used when attacking
        m_disableMovementTimer -= Time.deltaTime;

        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            jumpTimer = 0f;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            DOVirtual.DelayedCall(0.3f, () =>
            {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
            });
        }

        // -- Handle input and movement --
        float inputX = 0.0f;

        if (m_disableMovementTimer < 0.0f)
            inputX = Input.GetAxis("Horizontal");

        // GetAxisRaw returns either -1, 0 or 1
        float inputRaw = Input.GetAxisRaw("Horizontal");
        // Check if current move input is larger than 0 and the move direction is equal to the characters facing direction
        if (Mathf.Abs(inputRaw) > Mathf.Epsilon && Mathf.Sign(inputRaw) == m_facingDirection)
        {
            m_moving = true;
        }
        else
        {
            m_moving = false;
        }


        // 根据运动方向翻转Sprite朝向
        if (inputRaw > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }

        else if (inputRaw < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }


        float SlowDownSpeed = m_moving ? 1.0f : 0.5f;// 减速速度帮助玩家在停下时过渡
        // Set movement
        m_body2d.velocity = new Vector2(inputX * m_maxSpeed * SlowDownSpeed, m_body2d.velocity.y);

        // Set AirSpeed in animator
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // Set Animation layer for hiding sword
        int boolInt = m_State == SpiritState.Spiritual ? 1 : 0;
        m_animator.SetLayerWeight(1, boolInt);

        // -- Handle Animations --
        //Jump
        if (Input.GetButtonDown("Jump") && m_grounded && m_disableMovementTimer < 0.0f)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce * jumpStartPower);
            m_groundSensor.Disable(0.1f);
            jumpTimer += 0.1f;

        }
        else if (Input.GetButton("Jump") && !m_grounded)
        {
            if (jumpTimer >= jumpMaxTime || jumpTimer == 0)
            {
                return;
            }
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            jumpTimer += Time.deltaTime;

        }
        else if (Input.GetButtonUp("Jump") && !m_grounded)
        {
            jumpTimer = 0f;
        }

        //Run
        else if (m_moving)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);
    }

    // Function used to spawn a dust effect
    // All dust effects spawns on the floor
    // dustXoffset controls how far from the player the effects spawns.
    // Default dustXoffset is zero

    private void Spiritualize()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(m_State == SpiritState.Physical)
            {
                Spritualize?.Invoke();
                m_State = SpiritState.Spiritual;
            }else
            {
                DeSpritualize?.Invoke();
                m_State = SpiritState.Physical;
            }

        }

    }

    void SpawnDustEffect(GameObject dust, float dustXOffset = 0)
    {
        if (dust != null)
        {
            // Set dust spawn position
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * m_facingDirection, 0.0f, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(m_facingDirection, 1, 1);
        }
    }

    // Animation Events
    // These functions are called inside the animation files
    void AE_runStop()
    {
        AudioSource.PlayClipAtPoint(m_RunSounds[0], this.transform.position);
        // Spawn Dust
        float dustXOffset = 0.6f;
        SpawnDustEffect(m_RunStopDust, dustXOffset);
    }

    void AE_footstep()
    {
        int seed = UnityEngine.Random.Range(0, m_RunSounds.Length);
        AudioSource.PlayClipAtPoint(m_RunSounds[seed], this.transform.position);
    }

    void AE_Jump()
    {
        AudioSource.PlayClipAtPoint(m_JumpSound, this.transform.position);
        // Spawn Dust
        SpawnDustEffect(m_JumpDust);
    }

    void AE_Landing()
    {
        AudioSource.PlayClipAtPoint(m_LandSound, this.transform.position);
        // Spawn Dust
        SpawnDustEffect(m_LandingDust);
    }


}
