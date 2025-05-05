using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class invertWhichAttackState : PhysicEnemyConditional
{
    // Start is called before the first frame update
    public override TaskStatus OnUpdate()
    {
        if (!transform.GetComponent<attackState>().isAttackOne)
        {
            transform.GetComponent<attackState>().isAttackOne = true;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
