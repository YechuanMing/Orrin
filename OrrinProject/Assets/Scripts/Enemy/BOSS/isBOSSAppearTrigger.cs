using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class isBOSSAppearTrigger : PhysicEnemyConditional
{
    public override TaskStatus OnUpdate()
    {
        if (!transform.GetComponent<triggerBossAnim>().isTriggerAnim)
        {
            transform.GetComponent<triggerBossAnim>().isTriggerAnim = true;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
