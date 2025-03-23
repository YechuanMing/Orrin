using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class SwitchTarget : Action
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        // 切换目标点
        if (transform.GetComponent<PositionPoint>().currentPoint == transform.GetComponent<PositionPoint>().point[0])
        {
            transform.GetComponent<PositionPoint>().currentPoint = transform.GetComponent<PositionPoint>().point[1];
        }
        else
        {
            transform.GetComponent<PositionPoint>().currentPoint = transform.GetComponent<PositionPoint>().point[0];
        }

        return TaskStatus.Success; // 切换完成
    }
}


