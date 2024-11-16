using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWave : MonoBehaviour
{

    public int damage;

    [SerializeField]
    private float impulseParam=4;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Gotcha");
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Destructable>().Damage(damage);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - this.transform.position) * damage * impulseParam, ForceMode2D.Impulse);
        }
        else if(collision.gameObject.CompareTag("DestructableStuffs"))
        {
            collision.gameObject.GetComponent<Destructable>().Damage(1);

        }
    }
}
