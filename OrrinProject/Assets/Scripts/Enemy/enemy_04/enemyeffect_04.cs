using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
public class enemyeffect_04 : SpiritualEnemyAction
{
    public GameObject bombPref;
    // Start is called before the first frame update
    public override TaskStatus OnUpdate()
    {

        { 
            GameObject b = GameObject.Instantiate(bombPref,new Vector3(transform.position.x,transform.position.y-0.54f,transform.position.y), Quaternion.identity);
            Debug.Log(b);
        }
        return TaskStatus.Success;

    }
}
