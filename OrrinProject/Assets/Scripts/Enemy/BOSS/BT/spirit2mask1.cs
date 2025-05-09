using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class spirit2mask1 : Conditional
{
    public override TaskStatus OnUpdate()
    {
       if(!transform.GetComponent<spirit2WhichStateMask>().shocked1&& !transform.GetComponent<spirit2WhichStateMask>().shocked2)
            return TaskStatus.Success;
       
        return TaskStatus.Failure;
    }
}
