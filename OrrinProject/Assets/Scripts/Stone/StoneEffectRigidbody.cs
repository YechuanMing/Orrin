using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneEffectRigidbody : MonoBehaviour
{
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 判断碰撞到的对象是否是 Ground 层
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.bodyType = RigidbodyType2D.Kinematic;

            // 冻结位置和旋转
            rb.constraints = RigidbodyConstraints2D.FreezePositionX |
                             RigidbodyConstraints2D.FreezePositionY |
                             RigidbodyConstraints2D.FreezeRotation;
            transform.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

}
