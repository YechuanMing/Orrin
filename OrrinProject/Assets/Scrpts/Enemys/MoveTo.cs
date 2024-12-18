using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
public class MoveTo :Action
{
    public float speed = 0;
    public SharedTransform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if (Vector3.SqrMagnitude(transform.position - target.Value.position) < 0.1f)
        {
            return TaskStatus.Success;
        }
        transform.position = Vector3.MoveTowards(transform.position, target.Value.position, speed * Time.deltaTime);
        return TaskStatus.Running;
    }

}
