using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S01Bomb : MonoBehaviour
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private float repelForce;
    [SerializeField]
    [Range(0, 1)]
    private float followLerp;

    private void Update()
    {
     
        
        transform.right = Vector3.Lerp(transform.right, (PlayerController.Instance.transform.position - transform.position).normalized, followLerp * Time.deltaTime);
        
        transform.Translate(transform.right * 2*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Destructable>().Damage(damage);
            Vector3 vec = (collision.transform.position - this.transform.position);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(vec.x, vec.y) * repelForce, ForceMode2D.Impulse);
            Destroy(this);
        }
    }
}
