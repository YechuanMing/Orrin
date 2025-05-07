using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_body2bomb : MonoBehaviour
{
    public GameObject BombOne;
    public float BombOne_time = 0.6f;
    public GameObject BombTwo;
    public float BombTwo_time = 0.6f;
    public GameObject BombSpirits;
    public float radius = 5f;
    // Start is called before the first frame update
    public void bomb_body2Attack1()
    {
        Vector3 center = transform.position;
        bool isPlayerOnLeft = GameObject.FindGameObjectWithTag("Player").transform.position.x < gameObject.transform.position.x;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        float startX = transform.InverseTransformPoint(player.position).x;
        float[] offsets = { -1f, 0f, 1f, 2f };
        float[] timeOptions = { 1.2f, 1.4f, 1.6f, 1.8f };
        int bombCount = 4;
        // ÊµÀý»¯Õ¨µ¯
        for (int i = 0; i < bombCount; i++)
        {
            GameObject bomb = Instantiate(BombOne, center, Quaternion.identity);
            float randomTime = timeOptions[Random.Range(0, timeOptions.Length)];
            if(isPlayerOnLeft)
                 bomb.GetComponent<bodyTwoAttackOneBomb>().move(startX + offsets[i], -2.3f, randomTime);
            else
                bomb.GetComponent<bodyTwoAttackOneBomb>().move(-(startX + offsets[i]), -2.3f, randomTime);
        }

    }
    public void bomb_body2Attack2()
    {
        Vector3 center = transform.position;

        Instantiate(BombTwo, center, Quaternion.Euler(0, 0, 45));
        Instantiate(BombTwo, center, Quaternion.Euler(0, 0, 135));

        Instantiate(BombTwo, center, Quaternion.Euler(0, 0, 90));
        Instantiate(BombTwo, center, Quaternion.Euler(0, 0, 180));
    }

    public void bombSpirit()
    {
        Vector3 center = transform.position;
        GameObject bomb1 = Instantiate(BombSpirits, center + new Vector3(0.5f, 0, 0), Quaternion.identity);
        GameObject bomb2 = Instantiate(BombSpirits, center - new Vector3(0.5f, 0, 0), Quaternion.identity);
        GameObject bomb3 = Instantiate(BombSpirits, center + new Vector3(0f, 0.5f, 0), Quaternion.identity);
        GameObject bomb4 = Instantiate(BombSpirits, center - new Vector3(0f, 0.5f, 0), Quaternion.identity);
    }
}
