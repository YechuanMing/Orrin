using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class chasePlayer_05 : PhysicEnemyAction
{
    public float approachSpeed = 1f; // 靠近敌人的速度
   // public float sprintSpeed = 3f;  // 冲刺时的速度
    public float outOfCombatDistance = 5f; // 脱战距离
    public float trackOffsetY;
    //public float approachDistanceThreshold = 0.5f; // 当距离玩家小于这个值时进入冲刺状态
    //public float sprintDistanceThreshold = 2f; // 当距离玩家小于这个值时进入冲刺状态


    // Start is called before the first frame update
    public override TaskStatus OnUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("玩家引用丢失，敌人无法追踪！");
            return TaskStatus.Failure; // 如果没有玩家引用，返回失败
        }

        // 计算敌人与玩家之间的距离
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        //if (distanceToPlayer > outOfCombatDistance)
        //{
        //    // 如果距离玩家超过脱战距离，返回失败
        //    Debug.Log("敌人超出脱战距离，进入脱战状态");
        //    return TaskStatus.Failure;
        //}
        //else
        {
            // 如果在脱战距离内，敌人追赶玩家
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position+new Vector3(0,trackOffsetY,0), approachSpeed * Time.deltaTime);

            // 根据移动方向调整敌人朝向
            Vector3 currentScale = transform.localScale;
            if (directionToPlayer.x > 0)
            {
                currentScale.x = Mathf.Abs(currentScale.x);
            }
            else if (directionToPlayer.x < 0)
            {
                currentScale.x = -Mathf.Abs(currentScale.x);
            }
            transform.localScale = currentScale;

            return TaskStatus.Success; // 返回成功，表示敌人正在追赶玩家
        }
    }
}
