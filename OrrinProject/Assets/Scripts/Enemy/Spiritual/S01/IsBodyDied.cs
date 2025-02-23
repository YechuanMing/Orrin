using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class IsBodyDied : SpiritualEnemyConditional
{

    public override TaskStatus OnUpdate()
    {
        return enemyBase.isBodyDied ? TaskStatus.Success : TaskStatus.Failure;
    }
}
