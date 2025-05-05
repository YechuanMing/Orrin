using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class isBOSSSpiritAppearTrigger : PhysicEnemyConditional
{
    public override TaskStatus OnUpdate()
    {
        if (!transform.GetComponent<triggerBossSpiritAnim>().isTriggerAnim)
        {
            transform.GetComponent<triggerBossSpiritAnim>().isTriggerAnim = true;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
