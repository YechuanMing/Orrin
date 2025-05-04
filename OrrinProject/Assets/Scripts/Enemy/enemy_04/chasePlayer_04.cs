using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Linq;
using DG.Tweening;
public class chasePlayer_04 : PhysicEnemyAction
{
    public float initialUpwardSpeed = 6f;   // 起跳的Y速度
    public float gravity = -20f;            // 自定义重力（负值）

    private Vector2 velocity;
    private bool hasJumped = false;
    // Start is called before the first frame update
    public override void OnStart()
    {
       
        // 确定跳跃方向
        float direction = PlayerController.Instance.transform.position.x < transform.position.x ? -1 : 1;
        Vector2 playerPos = PlayerController.Instance.transform.position;
        Vector2 startPos = transform.position;
        float horizontalDistance = Mathf.Abs(startPos.x- playerPos.x) *0.7f;
        float upTime =  Mathf.Abs(initialUpwardSpeed/gravity);
        float horizontalSpeed = horizontalDistance / upTime;
        // 设置初速度（向上+向前）
        velocity = new Vector2(horizontalSpeed * direction, initialUpwardSpeed);

        animator.Play("jumpingToSky");
        hasJumped = true;

        // 关闭Unity的默认重力，确保只受我们控制
        rigidbody.gravityScale = 0;
    }

    public override TaskStatus OnUpdate()
    {
        if (!hasJumped) return TaskStatus.Failure;

        // 模拟重力加速度
        velocity.y += gravity * Time.deltaTime;

        // 应用速度
        rigidbody.velocity = velocity;

        // 到达最高点：y速度变为0或负数
        if (velocity.y <= 0f)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
    public override void OnEnd()
    {
        hasJumped = false;
        // 还原重力（可选）
        rigidbody.gravityScale = 1;
    }
    //public float jumpForce = 4f;//跳跃速度
    //public float buildupTime;//起跳时间
    //public float horizontalForce;//水平力度
    //public bool jumpStarted = false;
    //private Tween buildUpTween;
    //private Tween jumpTween;
    //public string animation;

    //public override void OnStart()
    //{
    //    Debug.Log(player);
    //    buildUpTween = DOVirtual.DelayedCall(buildupTime, StartJump, false);//可能没啥用
    //    animator.Play(animation);
    //}

    //private void StartJump()
    //{
    //    var direction = PlayerController.Instance.transform.position.x < transform.position.x ? -1 : 1;
    //    rigidbody.velocity = Vector2.zero; // 清空原有速度
    //    rigidbody.AddForce(new Vector2(horizontalForce * direction, jumpForce), ForceMode2D.Impulse);
    //    //jumpStarted = true;
    //    jumpTween = DOVirtual.DelayedCall(0.1f, () => { jumpStarted = true; }, false);
    //}
    //public override TaskStatus OnUpdate()
    //{
    //    if (jumpStarted&&rigidbody.velocity.y<=0f)
    //    {
    //        return TaskStatus.Success;
    //    }
    //    return TaskStatus.Running;
    //}

    //public override void OnEnd()
    //{
    //    buildUpTween.Kill();
    //    jumpTween.Kill();
    //    jumpStarted = false;
    //}
}
