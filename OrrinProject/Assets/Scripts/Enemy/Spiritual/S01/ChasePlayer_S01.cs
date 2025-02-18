using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class ChasePlayer_S01 : PhysicEnemyAction
{
    public float approachSpeed = 1f; // 靠近敌人的速度
    public float keepDistance = 3f;  // 与敌人保持的最小距离
    public float wanderDistance = 0.5f; // 在边界附近游走的范围
    public float heightOffset = 1f;
    public float outOfCombatDistance = 5f; // 脱战距离
    public override void OnStart()
    {
    }
    private void KeepWithinHeightRange()
    {
        // 确保怪物始终在高度范围内
        Vector3 currentPosition = transform.position;
        currentPosition.y = Mathf.Clamp(currentPosition.y, originPos.y - heightOffset, originPos.y + heightOffset);
        transform.position = currentPosition;
    }

    public override TaskStatus OnUpdate()
    {
        if (player)
        {
            Vector2 directionToEnemy = (player.transform.position - transform.position).normalized;
            float distanceToEnemy = Vector2.Distance(transform.position, player.transform.position);
            // 根据移动方向调整 scale 实现翻转
            Vector3 currentScale = transform.localScale;
            if (directionToEnemy.x > 0)
            {
                currentScale.x = Mathf.Abs(currentScale.x);
            }
            else if (directionToEnemy.x < 0)
            {
                currentScale.x = -Mathf.Abs(currentScale.x);
            }
            transform.localScale = currentScale;
            KeepWithinHeightRange();
            //if (distanceToEnemy > outOfCombatDistance)
            //{
            //    // 敌人超出脱战距离，输出日志并切换到随机游走状态
            //    Debug.Log("敌人超出脱战距离，怪物进入随机游走状态");
            //    return TaskStatus.Failure;
            //}
            if (distanceToEnemy > keepDistance)
            {
                // 如果距离敌人超过保持距离，靠近敌人
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, approachSpeed * Time.deltaTime);
                return TaskStatus.Success;
            }
            else
            {
                // 在保持距离的边界附近游走
                Vector2 randomOffset = Random.insideUnitCircle * wanderDistance;
                Vector2 targetPosition = (Vector2)player.transform.position + directionToEnemy * keepDistance + randomOffset;
                // 确保目标位置在高度范围内
                targetPosition.y = Mathf.Clamp(targetPosition.y, originPos.y - heightOffset, originPos.y + heightOffset);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, approachSpeed * Time.deltaTime);
                return TaskStatus.Failure;
            }
        }
        return TaskStatus.Failure;





    }
}
