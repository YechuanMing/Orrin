using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWave : MonoBehaviour
{

    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Gotcha");
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Destructable>().Damage(damage);
        }else if(collision.gameObject.CompareTag("DestructableStuffs"))
        {
            collision.gameObject.GetComponent<Destructable>().Damage(1);
        }
    }
}
