using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class IsSpiritState :SpiritualEnemyConditional
{

    public override TaskStatus OnUpdate()
    {
        return enemyBase.isSpirit ? TaskStatus.Success : TaskStatus.Failure;
    }
}
