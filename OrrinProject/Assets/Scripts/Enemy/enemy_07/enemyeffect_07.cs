using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class enemyeffect_07 : SpiritualEnemyAction
{
    public GameObject bombPref;
    public float radius = 4f;
    public override TaskStatus OnUpdate()
    {

        {
            Vector3 from = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(from.y, from.x) * Mathf.Rad2Deg; // 用于旋转
            float signedAngle = Vector2.SignedAngle(Vector2.right, from); // 有符号夹角
            // Step 3: 计算生成位置
            Vector3 spawnPos = player.transform.position - from * radius;
            // Step 4: 让 b 朝向它的飞行方向
            // Step 4: 计算旋转，让炸弹朝向玩家方向
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject b = GameObject.Instantiate(bombPref, spawnPos,rotation);
            Debug.Log(b);
        }
        return TaskStatus.Success;

    }
}
