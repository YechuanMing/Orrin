using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Linq;
using DG.Tweening;
public class chasePlayer_03 : PhysicEnemyAction
{
    public float jumpDuration = 1f; // 跳跃持续时间
    public float jumpForce = 4f;//跳跃速度
    public float buildupTime;//起跳时间
    public float horizontalForce;//水平力度

    private bool haslanded;

    private Tween buildUpTween;
    private Tween jumpTween;
    public override void OnStart()
    {
        Debug.Log(player);
        buildUpTween =DOVirtual.DelayedCall(buildupTime, StartJump, false);
        animator.Play("Jump");
    }

    private void StartJump()
    {
        var direction = PlayerController.Instance.transform.position.x < transform.position.x ? -1 : 1;
    
        rigidbody.AddForce(new Vector2(horizontalForce * direction, jumpForce), ForceMode2D.Impulse);

        jumpTween=DOVirtual.DelayedCall(jumpDuration, () => { haslanded = true; }, false);
    }
    float GetAnimationLength(string animationName)
    {
        AnimationClip clip = animator.runtimeAnimatorController.animationClips.FirstOrDefault(c => c.name == animationName);
        return clip != null ? clip.length : 0f;
    }
    public override TaskStatus OnUpdate()
    {

        return haslanded ? TaskStatus.Success : TaskStatus.Running;
    }

    public override void OnEnd()
    {
        buildUpTween.Kill();
        jumpTween.Kill();
        haslanded = false;
    }

}
