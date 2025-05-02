using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect_enemy07 : MonoBehaviour
{

    [SerializeField]
    public int touchDamage = 4;
    [SerializeField]
    private float repelForce;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Destructable>().Damage(touchDamage);
            Vector3 vec = (collision.transform.position - this.transform.position);
            int i = collision.transform.position.x > this.transform.position.x ? 1 : -1;

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((Vector2.right * i + Vector2.up) * repelForce, ForceMode2D.Impulse);

        }
    }
}
