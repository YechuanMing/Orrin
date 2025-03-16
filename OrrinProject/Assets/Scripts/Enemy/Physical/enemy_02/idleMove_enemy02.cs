using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class idleMove_enemy02 : SpiritualEnemyAction
{
    public SharedVector3 currentTarget;
    public float moveSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        // 计算移动方向
        Vector3 direction = (currentTarget.Value - transform.position).normalized;
        // 移动敌人
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        // 检查是否到达目标点
        if (Vector3.Distance(transform.position, currentTarget.Value) < 0.1f)
        {
            return TaskStatus.Success; // 到达目标点，返回成功
        }

        return TaskStatus.Running; // 仍在移动中
    }
}
