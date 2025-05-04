using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Linq;
using DG.Tweening;
public class jumpToGroundToPlayer : PhysicEnemyAction
{
    public float gravity = -15f;          // 自定义重力加速度
    private Vector2 velocity;
    private bool hasJumped = false;

    // Start is called before the first frame update
    public override void OnStart()
    {
        CalculateJumpVelocity();
        animator.Play("jumpingToGround");
        rigidbody.gravityScale = 0; // 禁用默认重力
    }
    private float CalculateHeightToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100f, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            return hit.distance;
        }
        else
        {
            Debug.LogWarning("Ground not found below!");
            return 0f;
        }
    }
    private void CalculateJumpVelocity()
    {
        Vector2 playerPos = PlayerController.Instance.transform.position;
        Vector2 startPos = transform.position;
        float height = CalculateHeightToGround();
        float fallTime = Mathf.Sqrt(2f * height / Mathf.Abs(gravity));
       
        // 水平位移和速度
        float horizontalDistance = playerPos.x - startPos.x;
        float horizontalSpeed = horizontalDistance / fallTime;

        velocity = new Vector2(horizontalSpeed, 0);
        hasJumped = true;
    }
  
    public override TaskStatus OnUpdate()
    {
        if (!hasJumped)
            return TaskStatus.Failure;

        // 模拟重力加速度
        velocity.y += gravity * Time.deltaTime;

        // 应用速度
        rigidbody.velocity = velocity;
        if (hasJumped && IsGrounded())
        {
            rigidbody.velocity = Vector2.zero;
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }

    public override void OnEnd()
    {
        hasJumped = false;
        rigidbody.gravityScale = 1; // 禁用默认重力
    }
}

