using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : PhysicEnemyAction
{
    private float baseScaleX;
    public bool revert;

    public override void OnAwake()
    {
        base.OnAwake();
        baseScaleX = transform.localScale.x;
    }
    public override TaskStatus OnUpdate()
    {
        var scale = transform.localScale;
        if (revert)
        { scale.x = transform.position.x < player.transform.position.x ? baseScaleX : -baseScaleX; }
        else
        { scale.x = transform.position.x < player.transform.position.x ? -baseScaleX : baseScaleX; }
        transform.localScale = scale;
        return TaskStatus.Success;
    }


}
