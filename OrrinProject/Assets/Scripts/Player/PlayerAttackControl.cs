using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackControl : MonoBehaviour
{

    public bool isFreeze;
    public static PlayerAttackControl Instance { get; private set; }
    private Animator m_animator;
    private Rigidbody2D m_body2d;
    [SerializeField]
    private float m_disablePhysicalAttackTimer = 0.0f;
    [SerializeField]
    private float m_PhysicalAttackCoolDownTime = 0.5f;

    // Animation Events 动画帧事件
    // 在角色动画中调用
    [Header("普通攻击")]
    [Header("攻击及技能")]
    [SerializeField]
    private GameObject AttackWave_Front;
    [SerializeField]
    private GameObject AttackWave_Up;
    [SerializeField]
    private GameObject AttackWave_Down;
    [SerializeField]
    private float attackDuration;
    [SerializeField]
    public Transform attackFrontSpot;
    [SerializeField]
    private Transform attackUpSpot;
    [SerializeField]
    private Transform attackDownSpot;

    [SerializeField]
    [Range(0.4f, 1)]
    private float attackFrontOffsetX;

    [SerializeField]
    [Range(0.4f, 1)]
    private float attackUpOffsetY;

    [SerializeField]
    [Range(0.4f, 1)]
    private float attackDownOffsetY;

    private void Awake()
    {
        // 检查是否已有实例
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 设置实例并标记为不销毁
        Instance = this;
    }

    void AE_Attack_Front()
    {
        GameObject attackWave = Instantiate(AttackWave_Front, attackFrontSpot.position + Vector3.right * attackFrontOffsetX * transform.localScale.x, attackFrontSpot.rotation);
        attackWave.transform.localScale = transform.localScale;
        attackWave.transform.SetParent(this.transform);
        Destroy(attackWave, attackDuration);
    }

    void AE_Attack_Up()
    {
        GameObject attackWave = Instantiate(AttackWave_Up, attackUpSpot.position + Vector3.up * attackUpOffsetY, attackUpSpot.rotation);
        attackWave.transform.localScale = transform.localScale;
        attackWave.transform.SetParent(this.transform);
        Destroy(attackWave, attackDuration);
    }

    void AE_Attack_Down()
    {
        GameObject attackWave = Instantiate(AttackWave_Down, attackDownSpot.position + Vector3.down * attackDownOffsetY, attackDownSpot.rotation);
        attackWave.transform.SetParent(this.transform);
        attackWave.transform.localScale = transform.localScale;
        Destroy(attackWave, attackDuration);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

    }

    public void SetFreeze(bool set)
    {
        isFreeze = set;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFreeze)
        {
            return;
        }

        if (m_disablePhysicalAttackTimer <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.W))
                {
                    //m_animator.SetTrigger("UpAttack");

                    m_animator.Play("UpAttack");
                }
                else if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.S))
                {
                    //m_animator.SetTrigger("DownAttack");
                    m_animator.Play("DownAttack");
                }
                else
                {
                    //m_animator.SetTrigger("FrontAttack");
                    m_animator.Play("FrontAttack");
                }
                m_disablePhysicalAttackTimer = m_PhysicalAttackCoolDownTime;
            }
        }
        else
        {
            m_disablePhysicalAttackTimer -= Time.deltaTime;
        }

    }
}
