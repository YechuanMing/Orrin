using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class GoToOriginalPoint : PhysicEnemyAction
{
    public float distanceThreshold;
    public float chaseSpeed;

    public override TaskStatus OnUpdate()
    {
        if (/*Mathf.Abs(originPos.x - transform.position.x)*/ Vector2.Distance(originPos,transform.position)<= distanceThreshold)
        {
            return TaskStatus.Success;
        }
        else
        {
            animator.Play("Walk");
            transform.Translate(transform.localScale.normalized * (-1f) * chaseSpeed * Time.deltaTime);
            return TaskStatus.Failure;
        }
    }
}
