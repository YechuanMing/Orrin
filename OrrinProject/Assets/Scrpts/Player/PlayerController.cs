using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour
{

    [Header("Variables")]

    [SerializeField] private float m_maxSpeed = 4.5f;
    [SerializeField] private float m_jumpForce = 7.5f;
    [SerializeField] [Range(0.5f, 1)] private float jumpMaxTime = 0.7f;
    [SerializeField] [Range(0.01f, 10)] private float jumpStartPower = 2f;
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

    [SerializeField]
    private float m_disablePhysicalAttackTimer = 0.0f;
    [SerializeField]
    private float m_PhysicalAttackCoolDownTime = 0.5f;



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


        // �����˶�����תSprite����
        if (inputRaw > 0)
        {
            //GetComponent<SpriteRenderer>().flipX = false;
            transform.localScale = new Vector3(1, 1, 1);
            m_facingDirection = 1;
        }

        else if (inputRaw < 0)
        {
            //GetComponent<SpriteRenderer>().flipX = true;
            transform.localScale = new Vector3(-1, 1, 1);
            m_facingDirection = -1;
        }


        float SlowDownSpeed = m_moving ? 1.0f : 0.5f;// �����ٶȰ��������ͣ��ʱ����

        // �����ٶ�
        m_body2d.velocity = new Vector2(inputX * m_maxSpeed * SlowDownSpeed, m_body2d.velocity.y);

        // ΪAnimator�еı�����ֵ
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // Set Animation layer for hiding sword
        //int boolInt = m_State == SpiritState.Spiritual ? 1 : 0;
        //m_animator.SetLayerWeight(1, boolInt);

        // -- ������� --
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

        //��
        else if (m_moving)
        {
            m_animator.SetInteger("AnimState", 1);
        }

        //��ֹ״̬
        else
        {
            m_animator.SetInteger("AnimState", 0);
        }

        //����

        if (m_disablePhysicalAttackTimer <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.GetMouseButtonDown(0)&&Input.GetKey(KeyCode.W))
                {
                    m_animator.SetTrigger("UpAttack");
                }
                else if (Input.GetMouseButtonDown(0)&&Input.GetKey(KeyCode.S))
                {
                    m_animator.SetTrigger("DownAttack");
                }
                else
                {
                    m_animator.SetTrigger("FrontAttack");
                }
                m_disablePhysicalAttackTimer = m_PhysicalAttackCoolDownTime;
            }
        }
        else
        {
            m_disablePhysicalAttackTimer -= Time.deltaTime;
        }

    }



    // �������ɻҳ�Ч���ķ���
    // �ҳ����ڵ�������
    // dustXoffset���Ե������ɾ��룬Ĭ��Ϊ0

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



    // Animation Events ����֡�¼�
    // �ڽ�ɫ�����е���
    [SerializeField]
    private GameObject AttackWave_Front;
    [SerializeField]
    private GameObject AttackWave_Up;
    [SerializeField]
    private GameObject AttackWave_Down;
    [SerializeField]
    private float attackDuration;
    [SerializeField]
    private Transform attackFrontSpot;
    [SerializeField]
    private Transform attackUpSpot;
    [SerializeField]
    private Transform attackDownSpot;


    void AE_Attack_Front()
    {
        GameObject attackWave = Instantiate(AttackWave_Front, attackFrontSpot.position, attackFrontSpot.rotation);
        Destroy(attackWave, attackDuration);
    }

    void AE_Attack_Up()
    {
        GameObject attackWave = Instantiate(AttackWave_Up, attackUpSpot.position,attackUpSpot.rotation);
        Destroy(attackWave, attackDuration);
    }

    void AE_Attack_Down()
    {
        GameObject attackWave = Instantiate(AttackWave_Down, attackDownSpot.position,attackDownSpot.rotation);
        Destroy(attackWave, attackDuration);
    }



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
