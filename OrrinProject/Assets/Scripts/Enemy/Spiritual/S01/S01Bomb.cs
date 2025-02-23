using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S01Bomb : MonoBehaviour
{

    public bool isSpiritBomb;

    [Header("伤害特性")]
    [SerializeField]
    private int damage_Phy;
    [SerializeField]
    private int damage_Spr=10;
    [SerializeField]
    private float repelForce;

    [Range(5, 10)]
    public float lifeTime;

    public Transform player;
    // 初始速度范围
    public float minInitialSpeed = 2f;
    public float maxInitialSpeed = 3f;
    // 追踪速度
    public float trackingSpeed = 2f;
    // 转向速度（度/秒）
    public float turnSpeed = 180f;
    private Rigidbody2D rb;
    private Vector2 initialDirection;
    private float initialSpeed;
    private void Awake()
    {
        // 获取刚体组件
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (isSpiritBomb)
        {
            player = PlayerSpiritControl.Instance.transform;
        }
        else
        {
            player = PlayerController.Instance.transform;
        }
        Destroy(this.gameObject, lifeTime);
        // 生成随机的初始方向
        initialDirection = Random.insideUnitCircle.normalized;
        // 生成随机的初始速度
        initialSpeed = Random.Range(minInitialSpeed, maxInitialSpeed);
        // 应用初始速度
        rb.velocity = initialDirection * initialSpeed;
    }
    private void FixedUpdate()
    {
        if (player != null)
        {
            // 计算炮弹到玩家的方向
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            // 计算当前速度方向
            Vector2 currentDirection = rb.velocity.normalized;
            // 计算需要旋转的角度
            float angleToTurn = Vector2.SignedAngle(currentDirection, directionToPlayer);
            // 限制旋转角度，使其不超过转向速度
            angleToTurn = Mathf.Clamp(angleToTurn, -turnSpeed * Time.fixedDeltaTime, turnSpeed * Time.fixedDeltaTime);
            // 旋转当前速度方向
            Quaternion rotation = Quaternion.Euler(0, 0, angleToTurn);
            currentDirection = rotation * currentDirection;
            // 更新炮弹的速度
            rb.velocity = currentDirection * trackingSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    Destroy(this.gameObject);
        //    return;
        //}
        if (isSpiritBomb)
        {
            PlayerSpiritualization.Instance.DamageSpirit(damage_Spr);

            PlayerSpiritualization.Instance.DeSpiritualize();
            Destroy(this.gameObject);
            
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Destructable>().Damage(damage_Phy);
            Vector3 vec = (collision.transform.position - this.transform.position);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(vec.x, vec.y) * repelForce, ForceMode2D.Impulse);
            Destroy(this.gameObject);
        }

        Destroy(this.gameObject);
    }
}



