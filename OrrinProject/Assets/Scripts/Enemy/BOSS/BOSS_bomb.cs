using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_bomb : MonoBehaviour
{
    public GameObject BombOne;
    public GameObject BombTwo;
    public GameObject BombSpirits;
    public float radius = 5f;
    public void bomb_body1Attack1()
    {
        Vector3 center = transform.position;
        // 实例化炸弹
        GameObject bomb = Instantiate(BombOne, center, Quaternion.identity);

        // 获取 Rigidbody2D 组件
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // 施加一个向上和向左的力
            Vector2 force = new Vector2(-3f, 5f); // 可根据需要调整方向和大小
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
    public void bomb_body1Attack2()
    {
        Vector3 center = transform.position;

    }
    public void bombSpirit()
    {
        Vector3 center = transform.position;
     
    }
}
