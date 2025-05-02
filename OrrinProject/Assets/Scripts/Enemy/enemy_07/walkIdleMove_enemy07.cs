using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class walkIdleMove_enemy07 : SpiritualEnemyAction
{
    public float wanderRadius = 2f; // 小范围移动的半径
    public float moveSpeed = 1f;    // 移动速度
    public float changeDirectionInterval = 3f; // 改变移动方向的时间间隔
    public float heightOffset = 2f;
    private float currentSpeed;
    public float decelerationDistance = 0.5f;
    public float maxMoveSpeed = 2f;
    private Vector2 targetPosition;
    private float timer;

    private string currentAnim = ""; // 当前播放的动画名称

    public override void OnStart()
    {
        animator.Play("walkandidle");
    }
    private void SetNewTargetPosition()
    {
        // 以当前位置为中心，在半径范围内随机选择一个目标位置
        Vector2 randomOffset = Random.insideUnitCircle * wanderRadius;
        targetPosition = (Vector2)transform.position + randomOffset;
        // 确保目标位置在高度范围内
        targetPosition.y = Mathf.Clamp(targetPosition.y, originPos.y - heightOffset, originPos.y + heightOffset);
    }
    private void MoveTowardsTarget()
    {
        // 向目标位置移动
        float distanceToTarget = Vector2.Distance(transform.position, targetPosition);
        // 根据距离目标的远近调整速度，实现减速效果
        if (distanceToTarget < decelerationDistance)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * (decelerationDistance / distanceToTarget));

        }
        else
        {
            currentSpeed = maxMoveSpeed;
            
        }
        // 计算移动方向
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        // 根据移动方向调整 scale 实现翻转

        Vector3 currentScale = transform.localScale;
        if (direction.x > 0)
        {
            currentScale.x = Mathf.Abs(currentScale.x);
        }
        else if (direction.x < 0)
        {
            currentScale.x = -Mathf.Abs(currentScale.x);
        }
        transform.localScale = currentScale;

        // 移动怪物
        transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);
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
        timer += Time.deltaTime;
        if (timer >= changeDirectionInterval)
        {
            SetNewTargetPosition();
            timer = 0f;
        }
        MoveTowardsTarget();
        //KeepWithinHeightRange();
        return TaskStatus.Success;
    }
}
