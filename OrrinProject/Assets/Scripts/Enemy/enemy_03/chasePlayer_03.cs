using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Linq;
public class chasePlayer_03 : PhysicEnemyAction
{
    public float distanceThreshold=0.4f;
    public float jumpSpeed=4f;//跳跃速度
    public float outOfCombatDistance = 5f;//脱战距离
    public float distanceToPlayer;
    public Vector3 PlayerPos;

    public float jumpHeight = 2f; // 跳跃高度
    public float jumpDuration = 1f; // 跳跃持续时间
    private bool isJumping = false; // 是否正在跳跃
    public override void OnStart()
    {
        PlayerPos = player.transform.position;
        distanceToPlayer = Mathf.Abs(player.transform.position.x - transform.position.x);
        // 获取跳跃动画的持续时间
        jumpDuration = GetAnimationLength("enemyBody03_jump");
    }
    float GetAnimationLength(string animationName)
    {
        AnimationClip clip = animator.runtimeAnimatorController.animationClips.FirstOrDefault(c => c.name == animationName);
        return clip != null ? clip.length : 0f;
    }
    public override TaskStatus OnUpdate()
    {
        if(distanceToPlayer<=outOfCombatDistance)
        {
            if (!isJumping)
            {
                isJumping = true;
               
                animator.Play("enemyBody03_jump");
                float jumpForce = (2f * jumpHeight) / jumpDuration;
                Vector2 jumpDirection = (PlayerPos - transform.position).normalized;
                rigidbody.velocity = new Vector2(jumpDirection.x * jumpSpeed, jumpForce);
            }
            if (Vector3.Distance(transform.position, PlayerPos) <= 0.1f)
            {
                isJumping = false;
                return TaskStatus.Success;
            }
            //敌人跳到目标位置之后，返回success，
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Success;
        }
    }
}
