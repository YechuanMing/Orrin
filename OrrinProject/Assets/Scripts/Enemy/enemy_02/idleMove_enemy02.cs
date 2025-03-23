using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class idleMove_enemy02 : SpiritualEnemyAction
{
    //public SharedGameObject currentTarget;
    public float moveSpeed = 1f;

    public SharedGameObject targetGameObject;
    public SharedString stateName;
    public int layer = -1;
    public float normalizedTime = float.NegativeInfinity;
    // Start is called before the first frame update
    public override void OnStart()
    {
        var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            animator = currentGameObject.GetComponent<Animator>();
        animator.Play(stateName.Value, layer, normalizedTime);
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        // 计算移动方向
        Vector3 direction = (transform.GetComponent<PositionPoint>().currentPoint.transform.position - transform.position).normalized;

        // 根据移动方向改变敌人的朝向
        UpdateFacingDirection(direction);
        // 移动敌人
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        // 检查是否到达目标点
        if (Vector3.Distance(transform.position, transform.GetComponent<PositionPoint>().currentPoint.transform.position) < 1f)
        {
            return TaskStatus.Success; // 到达目标点，返回成功
        }
        return TaskStatus.Running; // 仍在移动中
    }
    private void UpdateFacingDirection(Vector3 direction)
    {
        
        // 如果移动方向的 x 分量大于 0，敌人面向右
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        // 如果移动方向的 x 分量小于 0，敌人面向左
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
