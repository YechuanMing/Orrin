using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class SwitchTarget : Action
{
    public SharedVector3 targetPositionA; // 目标点A
    public SharedVector3 targetPositionB; // 目标点B
    public SharedVector3 currentTarget; // 当前目标点
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        // 切换目标点
        if (currentTarget.Value == targetPositionA.Value)
        {
            currentTarget.Value = targetPositionB.Value;
        }
        else
        {
            currentTarget.Value = targetPositionA.Value;
        }

        return TaskStatus.Success; // 切换完成
    }
}


