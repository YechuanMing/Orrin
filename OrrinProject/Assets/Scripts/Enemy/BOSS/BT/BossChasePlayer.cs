using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class BossChasePlayer : PhysicEnemyAction
{
    public float distanceThreshold;
    public float chaseSpeed;
    // Start is called before the first frame update
    public override TaskStatus OnUpdate()
    {
        float distanceX = player.transform.position.x - transform.position.x;
        if (Mathf.Abs(distanceX) <= distanceThreshold)
        {
            return TaskStatus.Success;
        }
        else
        {
            animator.Play("Walk");

            // 判断主角在左还是右
            float direction = Mathf.Sign(distanceX); // -1 表示在左，1 表示在右

            // 只在X轴移动
            transform.Translate(new Vector3(direction * chaseSpeed * Time.deltaTime, 0f, 0f));

            return TaskStatus.Failure;
        }
    }
}