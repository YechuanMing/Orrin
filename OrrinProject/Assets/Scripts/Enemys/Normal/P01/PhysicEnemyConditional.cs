using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class PhysicEnemyConditional :Conditional
{
    protected SpiritualEnemyBase enemyBase;
    protected Destructable destructable_Body;
    protected Destructable destructable_Spirit;
    protected PlayerController player;
    protected Rigidbody2D rigidbody;
    protected Animator animator;
    protected bool isDetectPlayer;
    protected MoveZone moveZone;
    public override void OnAwake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        destructable_Body = GetComponent<Destructable>();
        destructable_Spirit = gameObject.GetComponentInChildren<Destructable>();
        player = PlayerController.Instance;
        animator = GetComponent<Animator>();
        enemyBase = GetComponent<SpiritualEnemyBase>();
        moveZone = GetComponent<MoveZone>();
    }

    public override void OnStart()
    {
        player = PlayerController.Instance;
    }

}
