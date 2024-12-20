using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class IsSpiritDied : PhysicEnemyConditional
{

    public override TaskStatus OnUpdate()
    {
        return enemyBase.isSpiritDied ? TaskStatus.Success : TaskStatus.Failure;
    }
}
