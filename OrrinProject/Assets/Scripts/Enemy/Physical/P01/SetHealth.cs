using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class SetHealth : PhysicEnemyAction
{

    public int remainHealth;

    public override TaskStatus OnUpdate()
    {
        destructable_Body.CurrHealth = remainHealth;
        return TaskStatus.Success;
    }
}
