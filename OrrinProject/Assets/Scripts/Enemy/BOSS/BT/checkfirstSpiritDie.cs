using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class checkfirstSpiritDie : PhysicEnemyAction
{
    private int num;//Áé»ê×´Ì¬ÏÂµÄÑªÁ¿
    private bool thirdBeShocked = false;
    // Start is called before the first frame update
    public override void OnStart()
    {
        num = transform.GetComponent<SoulsBeDestroyInStage>().BeDestroyNum;
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if (num == 3 && !thirdBeShocked)
        {
            thirdBeShocked = true;
            return TaskStatus.Success;
        }
      
        return TaskStatus.Failure;
    }
}
