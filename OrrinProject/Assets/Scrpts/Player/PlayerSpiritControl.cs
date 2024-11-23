using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerSpiritControl : MonoBehaviour
{


    [Header("����")]
    [SerializeField] private float m_maxSpeed = 4.5f;
    [SerializeField] private float spawnOffsetY = 0.1f;

    private Animator m_animator;
    private Rigidbody2D m_rbody2d;
    private bool m_moving = false;
    private int m_facingDirection = 1;
    private float m_disableMovementTimer = 0.0f;


    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float inputY = Input.GetAxis("Vertical");

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        // GetAxisRaw returns either -1, 0 or 1
        float inputRaw = Input.GetAxisRaw("Horizontal");

        // ��鵱ǰ�ƶ������Ƿ�Ϊ0���ҵ�ǰ�ƶ������Ƿ�ͳ���һ��
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
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }

        else if (inputRaw < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            m_facingDirection = -1;
        }


        float SlowDownSpeed = m_moving ? 1.0f : 0.5f;// �����ٶȰ��������ͣ��ʱ����

        // �����ٶ�
        m_rbody2d.velocity = new Vector2(inputX * m_maxSpeed * SlowDownSpeed, inputY * m_maxSpeed * SlowDownSpeed);

        if (Input.GetMouseButtonDown(0))
        {
            NormalAttack();
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

    Tween dashTween;

    [SerializeField][Range(0.2f,2f)]
    private float basicAttackDashDistance = 1f;

    [SerializeField]
    [Range(0.2f, 1f)]
    private float attackVelocityMultipier = 0.5f;

    [SerializeField]
    [Range(0.2f, 0.8f)]
    private float dashDuration=0.5f;
    private void NormalAttack()
    {
        m_animator.SetTrigger("Attack");
        Vector3 des = new Vector3(transform.position.x + m_rbody2d.velocity.x * attackVelocityMultipier + basicAttackDashDistance * m_facingDirection, transform.position.y + m_rbody2d.velocity.y * attackVelocityMultipier, transform.position.z);
        dashTween.Kill();
        dashTween = transform.DOMove(des, dashDuration).SetEase(Ease.OutSine);
    }

    public void ResetPos()
    {
        transform.localPosition = Vector3.zero + Vector3.up * spawnOffsetY;
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero + Vector3.up;
    }

}
