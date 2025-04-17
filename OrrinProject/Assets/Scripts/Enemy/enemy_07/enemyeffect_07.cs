using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class enemyeffect_07 : SpiritualEnemyAction
{
    public GameObject bombPref;
    public float radius = 4f;
    public float angleOffset = 30f; // 角度偏移（单位：度）
    public override TaskStatus OnUpdate()
    {

        {
            Vector3 baseDir = (transform.position - player.transform.position).normalized;
            Vector3 rotatedDir;
            // 如果是 2D 游戏，绕 Z 轴旋转（Unity2D 是 XY 平面）
            rotatedDir = Quaternion.Euler(0f, 0f, angleOffset) * baseDir;
            // Step 3: 计算生成位置
            Vector3 spawnPos = transform.position + rotatedDir * radius;
            // Step 4: 让 b 朝向它的飞行方向
            float angle = Mathf.Atan2(rotatedDir.y, rotatedDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            GameObject b = GameObject.Instantiate(bombPref, spawnPos,rotation);
            Debug.Log(b);
        }
        return TaskStatus.Success;

    }
}
