using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceOriginalPoint: PhysicEnemyAction
{
    private float baseScaleX;

    public override void OnAwake()
    {
        base.OnAwake();
        baseScaleX = transform.localScale.x;
    }
    public override TaskStatus OnUpdate()
    {
        var scale = transform.localScale;
        scale.x = transform.position.x < originPos.x ? -baseScaleX : baseScaleX;
        transform.localScale = scale;
        return TaskStatus.Success;
    }


}
