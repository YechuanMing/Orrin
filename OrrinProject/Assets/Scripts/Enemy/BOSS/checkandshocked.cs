using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkandshocked : PhysicEnemyAction
{
    private int num;//Áé»ê×´Ì¬ÏÂµÄÑªÁ¿
    private bool firstBeShocked = false;
    private bool secondBeShocked = false;
    // Start is called before the first frame update
    public override void  OnStart()
    {
        num = transform.GetComponent<SoulsBeDestroyInStage>().BeDestroyNum;
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if(num==2 && !secondBeShocked)
        {
            secondBeShocked = true;
            return TaskStatus.Success;
        }
        else if(num == 1 && !firstBeShocked)
        {
            firstBeShocked = true;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
