using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;


public class FireBomb_S01 : SpiritualEnemyAction
{
    public GameObject bombPref;

    public override TaskStatus OnUpdate()
    {

        {
             GameObject b=GameObject.Instantiate(bombPref, transform.position, transform.rotation);
            Debug.Log(b);
        }
        return TaskStatus.Success;
        
    }
}
