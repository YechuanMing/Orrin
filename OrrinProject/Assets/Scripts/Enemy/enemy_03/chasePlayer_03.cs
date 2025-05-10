using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Linq;
using DG.Tweening;
using Cinemachine;
public class chasePlayer_03 : PhysicEnemyAction
{
    public float jumpForce = 4f;//跳跃速度
    public float buildupTime;//起跳时间
    public float horizontalForce;//水平力度
    public bool jumpStarted = false;
    private bool haslanded = false;
    private Tween buildUpTween;
    private Tween jumpTween;

    public override void OnStart()
    {
        
        Debug.Log(player);
        buildUpTween =DOVirtual.DelayedCall(buildupTime, StartJump, false);//可能没啥用
        animator.Play("jumping");
    }
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, LayerMask.GetMask("Ground","Default","PlayerBody"));
        return hit.collider != null;
    }
    private void StartJump()
    {
        var direction = PlayerController.Instance.transform.position.x < transform.position.x ? -1 : 1;
        float distanceParam = 0.3f*Vector2.Distance(PlayerController.Instance.transform.position, transform.position);

        rigidbody.AddForce(new Vector2(horizontalForce * direction*distanceParam, jumpForce), ForceMode2D.Impulse);
        jumpStarted = true;
        jumpTween=DOVirtual.DelayedCall(1f, () => { haslanded = true;GetComponent<CinemachineCollisionImpulseSource>().GenerateImpulse(); }, false);
    }
    public override TaskStatus OnUpdate()
    {
        if (IsGrounded()&&jumpStarted&&haslanded)
        {
           return TaskStatus.Success;
        } 
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        buildUpTween.Kill();
        jumpTween.Kill();
        jumpStarted = false;
        haslanded = false;
    }

}
