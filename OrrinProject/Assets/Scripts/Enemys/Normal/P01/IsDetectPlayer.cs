using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class IsDetectPlayer : PhysicEnemyConditional
{
    public float frontDis = 8f;
    public float backDis = 5f;
    public float UpDis = 6f;
    public float lostChaseDis = 8f;

    public override TaskStatus OnUpdate()
    {
        if(!player)
        {
            return TaskStatus.Failure;
        }

        if (Mathf.Abs(player.transform.position.y - transform.position.y) < UpDis)
        {
            if (transform.localScale.x < 0)
            {

                if (player.transform.position.x < transform.position.x)
                {
                    //怪物向右看，玩家在左侧
                    if (Mathf.Abs(PlayerController.Instance.transform.position.x - transform.position.x) <= backDis)
                    {

                        return TaskStatus.Success;
                    }
                }

                if (player.transform.position.x >= transform.position.x)
                {
                    //怪物向右看，玩家在右侧
                    if (Mathf.Abs(player.transform.position.x - transform.position.x) <= frontDis)
                    {
 
                        return TaskStatus.Success;
                    }

                }
            }
            else if (transform.localScale.x > 0)
            {
                if (player.transform.position.x < transform.position.x)
                {
                    //怪物向左看，玩家在左侧
                    if (Mathf.Abs(player.transform.position.x - transform.position.x) <= frontDis)
                    {

                        return TaskStatus.Success;

                    }
                }

                if (player.transform.position.x >= transform.position.x)
                {
                    //怪物向左看，玩家在右侧
                    if (Mathf.Abs(player.transform.position.x - transform.position.x) <= backDis)
                    {
 
                        return TaskStatus.Success;
                    }

                }
            }
        }

        return TaskStatus.Failure;


    }
}
