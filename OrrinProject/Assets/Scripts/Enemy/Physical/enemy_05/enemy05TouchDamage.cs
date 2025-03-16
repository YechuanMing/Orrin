using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy05TouchDamage : MonoBehaviour
{
    private SpiritualEnemyBase m_base;
    [SerializeField]
    public int touchDamage = 1;
    [SerializeField]
    private float repelForce;

    private void Awake()
    {
        m_base = GetComponentInParent<SpiritualEnemyBase>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_base.isBodyDied || m_base.isSpiritDied)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Destructable>().Damage(touchDamage);
            Vector3 vec = (collision.transform.position - this.transform.position);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(vec.x, vec.y) * repelForce, ForceMode2D.Impulse);
        }
    }
}
