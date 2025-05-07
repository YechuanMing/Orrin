using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_bomb : MonoBehaviour
{
    public GameObject BombOne;
    public float BombOne_time = 0.6f;
    public GameObject BombTwo;
    public float BombTwo_time = 0.6f;
    public GameObject BombSpirits;
    public float radius = 5f;
    public void bomb_body1Attack1()
    {
        Vector3 center = transform.position;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        float startX =Mathf.Abs(transform.InverseTransformPoint(player.position).x);
        // ÊµÀý»¯Õ¨µ¯
        GameObject bomb1 = Instantiate(BombOne, center, Quaternion.identity);
        bomb1.GetComponent<bodyOneAttackOneBomb>().move(startX, -2.3f, BombOne_time);
        GameObject bomb2 = Instantiate(BombOne, center, Quaternion.identity);
        bomb2.GetComponent<bodyOneAttackOneBomb>().move(startX+3f, -2.3f, BombOne_time);
        GameObject bomb3 = Instantiate(BombOne, center, Quaternion.identity);
        bomb3.GetComponent<bodyOneAttackOneBomb>().move(-startX, -2.3f, BombOne_time);
        GameObject bomb4 = Instantiate(BombOne, center, Quaternion.identity);
        bomb4.GetComponent<bodyOneAttackOneBomb>().move(-startX-3f, -2.3f, BombOne_time);

    }
    public void bomb_body1Attack2()
    {
        bool isPlayerOnLeft = GameObject.FindGameObjectWithTag("Player").transform.position.x < gameObject.transform.position.x;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        float moveStartX = transform.InverseTransformPoint(player.position).x ;//Îó´òÎó×²ÄÜËø¶¨ µ«Âß¼­ºÃÆæ¹Ö
        Vector3 center = transform.position;
        int bombCount = 8;
        GameObject[] bombs = new GameObject[bombCount];

        float[] timeOptions = { BombTwo_time - 0.1f, BombTwo_time, BombTwo_time + 0.1f };

        if (isPlayerOnLeft)
        {
            for (int i = 0; i < bombCount; i++)
            {
                float x = moveStartX + 1.5f- i * 0.5f;
                bombs[i] = Instantiate(BombTwo, center, Quaternion.identity);
                float randomTime = timeOptions[Random.Range(0, timeOptions.Length)];
                bombs[i].GetComponent<bodyOneAttackTwoBomb>().move(x, -2.3f, randomTime);
            }
        }
        else
        {
            for (int i = 0; i < bombCount; i++)
            {
                float x = moveStartX - 1.5f + i * 0.5f;
                bombs[i] = Instantiate(BombTwo, center, Quaternion.identity);
                float randomTime = timeOptions[Random.Range(0, timeOptions.Length)];
                bombs[i].GetComponent<bodyOneAttackTwoBomb>().move(x, -2.3f, randomTime);
            }
        }
    }
    public void bombSpirit()
    {
        Vector3 center = transform.position;
        GameObject bomb1 = Instantiate(BombSpirits, center + new Vector3(1f, 2f, 0), Quaternion.identity);
        GameObject bomb2 = Instantiate(BombSpirits, center - new Vector3(1f,2f, 0), Quaternion.identity);
    }
}
