using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackObj : MonoBehaviour
{

    [SerializeField]
    public int damage = 5;
    [SerializeField]
    public int touchDamage = 3;

    [SerializeField]
    private float repelForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Destructable>().Damage(damage);
            Vector3 vec = (collision.transform.position - this.transform.position);
            Debug.Log("vec=" + vec);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(vec.x, 0.5f) * repelForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Destructable>().Damage(touchDamage);
            Vector3 vec = (collision.transform.position - this.transform.position);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(vec.x,vec.y) * repelForce,ForceMode2D.Impulse);
        }
    }
}
