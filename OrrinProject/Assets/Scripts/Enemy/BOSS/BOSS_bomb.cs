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
        // ÊµÀý»¯Õ¨µ¯
        GameObject bomb1 = Instantiate(BombOne, center, Quaternion.identity);
        GameObject bomb2 = Instantiate(BombOne, center, Quaternion.identity);
        GameObject bomb3 = Instantiate(BombOne, center, Quaternion.identity);
        GameObject bomb4 = Instantiate(BombOne, center, Quaternion.identity);
        
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
