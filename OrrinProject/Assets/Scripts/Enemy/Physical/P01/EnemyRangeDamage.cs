using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeDamage : MonoBehaviour
{
    private SpiritualEnemyBase m_base;
    [SerializeField]
    public int damage = 5;
    [SerializeField]
    private float repelForce;

    private void Awake()
    {
        m_base = GetComponentInParent<SpiritualEnemyBase>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_base.isBodyDied || m_base.isSpiritDied)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Destructable>().Damage(damage);
            Vector3 vec = (collision.transform.position - this.transform.position);
            Debug.Log("vec=" + vec);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(vec.x, 0.5f) * repelForce, ForceMode2D.Impulse);
        }
    }
}
