using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class enemy05MinDistanceJudge : PhysicEnemyConditional
{
    public float approachDistance = 0.2f;

   
    public override TaskStatus OnUpdate()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= approachDistance)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
