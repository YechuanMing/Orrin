using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class NormalEnemyConditional : Conditional
{
    protected PlayerController playerController;

    protected Destructable destructable;
    protected Animator animator;
    protected Rigidbody2D rigidbody2D;

    protected Spiritualize_Enemy spiritualization;

    public override void OnAwake()
    {
        //playerController=PlayerController.Instance;
        animator = gameObject.GetComponentInChildren<Animator>();
        rigidbody2D = gameObject.GetComponentInParent<Rigidbody2D>();
        destructable = GetComponent<Destructable>();
        spiritualization =gameObject.GetComponentInParent<Spiritualize_Enemy>();

    }

}
