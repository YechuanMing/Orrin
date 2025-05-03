using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class checkandshocked2 : PhysicEnemyAction
{
    private int health;//Áé»ê×´Ì¬ÏÂµÄÑªÁ¿
    private bool firstBeShocked = false;
    private bool secondBeShocked = false;
    // Start is called before the first frame update
    public override void OnStart()
    {
        health = transform.GetComponent<SoulsBeDestroyInStage2>().destructable.currHealth;
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if (health <= transform.GetComponent<SoulsBeDestroyInStage2>().secondBeDestroyHealth && !secondBeShocked)
        {
            secondBeShocked = true;
            return TaskStatus.Success;
        }
        else if (health <= transform.GetComponent<SoulsBeDestroyInStage2>().firstBeDestroyHealth && !firstBeShocked)
        {
            firstBeShocked = true;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
