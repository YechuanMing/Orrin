using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class facePlayer_enemy07 : PhysicEnemyAction
{
    private float baseScaleX;
    private Vector3 lastPlayerPosition;
    public bool revert;
   
    public override void OnAwake()
    {
        base.OnAwake();
        baseScaleX = transform.localScale.x;
        if (player != null)
        {
            lastPlayerPosition = player.transform.position;
        }
    }
    public override TaskStatus OnUpdate()
    {
        if (Mathf.Abs(player.transform.position.x-lastPlayerPosition.x) > 0.01f)
        {
            var scale = transform.localScale;
            if (revert)
            { scale.x = transform.position.x < player.transform.position.x ? baseScaleX : -baseScaleX; }
            else
            { scale.x = transform.position.x < player.transform.position.x ? -baseScaleX : baseScaleX; }
            transform.localScale = scale;
            // 更新记录
            lastPlayerPosition = player.transform.position;
        }


        // 还在调整方向
        return TaskStatus.Running;
        
    }
}
