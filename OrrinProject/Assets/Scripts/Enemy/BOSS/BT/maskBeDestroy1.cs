using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class maskBeDestroy1 : Conditional
{
    public GameObject target;
    public bool shocked1 = false;
    public override TaskStatus OnUpdate()
    {
        if (GetComponent<Destructable>().currHealth==15&&!shocked1
            /*target.transform.GetComponent<spirit2WhichStateMask>().shocked1 && !target.transform.GetComponent<spirit2WhichStateMask>().shocked2&&!shocked1*/)
        {
            Debug.Log("Ãæ¾ßÁé»ê±»»÷Ëé");
            shocked1 = true;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
