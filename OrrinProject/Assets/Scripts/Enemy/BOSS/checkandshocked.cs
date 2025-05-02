using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkandshocked : PhysicEnemyAction
{
    private int health;//Áé»ê×´Ì¬ÏÂµÄÑªÁ¿
    private bool firstBeShocked = false;
    private bool secondBeShocked = false;
    // Start is called before the first frame update
    public override void  OnStart()
    {
        health = transform.GetComponent<SoulsBeDestroyInStage>().destructable.currHealth;
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if(health<= transform.GetComponent<SoulsBeDestroyInStage>().secondBeDestroyHealth && !secondBeShocked)
        {
            secondBeShocked = true;
            return TaskStatus.Success;
        }
        else if(health <= transform.GetComponent<SoulsBeDestroyInStage>().firstBeDestroyHealth && !firstBeShocked)
        {
            firstBeShocked = true;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
