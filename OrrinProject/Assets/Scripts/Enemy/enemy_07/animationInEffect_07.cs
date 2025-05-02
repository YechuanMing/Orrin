using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationInEffect_07 : MonoBehaviour
{
    public GameObject bombPref;
    private float radius = 4f;
    // Start is called before the first frame update

    public void effectAttack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 from = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(from.y, from.x) * Mathf.Rad2Deg; // 用于旋转
        float distance = Vector2.Distance(player.transform.position, transform.position);//玩家和敌人的距离
        float signedAngle = Vector2.SignedAngle(Vector2.right, from); // 有符号夹角
                                                                      // Step 3: 计算生成位置
        radius =1f+ distance / 2f;
        Vector3 spawnPos = player.transform.position - from * radius;
        // Step 4: 让 b 朝向它的飞行方向
        // Step 4: 计算旋转，让炸弹朝向玩家方向
        float biggerScale = distance / 2.8f;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        GameObject b = GameObject.Instantiate(bombPref, spawnPos, rotation);
        b.transform.localScale = new Vector3(1f * biggerScale, 1f, 1f);
        Debug.Log(b);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
