using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class ChasePlayer_Horizontal : PhysicEnemyAction
{
    public float distanceThreshold;
    public float chaseSpeed;
    public override TaskStatus OnUpdate()
    {
        if(Mathf.Abs(player.transform.position.x-transform.position.x)<=distanceThreshold)
        {
            return TaskStatus.Success;
        }else
        {
            animator.Play("Walk");
            transform.Translate(transform.localScale.normalized * (-1f) * chaseSpeed*Time.deltaTime);
            return TaskStatus.Failure;
        }
    }
}
