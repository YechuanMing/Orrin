using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class maskBeDestroy2 : Conditional
{
    public GameObject target;
    public bool shocked2 = false;
    public override TaskStatus OnUpdate()
    {
        if (target.transform.GetComponent<spirit2WhichStateMask>().shocked1 && target.transform.GetComponent<spirit2WhichStateMask>().shocked2 && !shocked2)
        {
            shocked2 = true;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
