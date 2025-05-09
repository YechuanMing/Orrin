using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkandshocked : Conditional
{
    //public int num;//灵魂状态下的血量
    public bool firstBeShocked = false;
    public bool secondBeShocked = false;
    // Start is called before the first frame update
    public override void  OnStart()
    {
      
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if (gameObject.transform.GetComponent<SoulsBeDestroyInStage>().shocked1 && !firstBeShocked)
        {
            Debug.Log("shocked1 成立，打断执行！");
            firstBeShocked = true;
            return TaskStatus.Success;
        }
        else if(gameObject.transform.GetComponent<SoulsBeDestroyInStage>().shocked2 && !secondBeShocked)
        {
            Debug.Log("shocked2 成立，打断执行！");
            secondBeShocked = true;
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
