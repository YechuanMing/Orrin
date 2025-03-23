using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

using DG.Tweening;
public class enemy05Sprint : PhysicEnemyAction
{
    public float buildupTime;
    public float sprintSpeed = 3f;  // 冲刺时的速度
    private bool hasEnded;
    public float sprintDuration = 1f; // 跳跃持续时间
    private Tween buildUpTween;
    private Tween sprintTween;                    // Start is called before the first frame update
    public float trackOffsetY=0.3f;
    public float upFly = 1.5f;
    public float upFlyDuration = 1f;
    // Update is called once per frame
    public override void OnStart()
    {
        //PlayerPos = player.transform.position;
        //distanceToPlayer = Mathf.Abs(player.transform.position.x - transform.position.x);
        //// 获取跳跃动画的持续时间
        //jumpDuration = GetAnimationLength("enemyBody03_jump");
        Debug.Log(player);
        buildUpTween = DOVirtual.DelayedCall(buildupTime, StartSprint, false);
        animator.Play("Jump");
    }

    private void StartSprint() { 
    
        //var direction = PlayerController.Instance.transform.position.x < transform.position.x ? -1 : 1;

        transform.DOMove(new Vector3(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.y+trackOffsetY, transform.position.z), sprintDuration)
        .SetEase(Ease.InCubic)    .OnComplete(()=> {
            transform.DOMove(transform.position + Vector3.up * upFly, upFlyDuration).SetEase(Ease.Linear);
        });

        sprintTween = DOVirtual.DelayedCall(sprintDuration, () => { hasEnded = true; }, false);
    }
    public override TaskStatus OnUpdate()
    {

        return hasEnded ? TaskStatus.Success : TaskStatus.Running;

    }

    public override void OnEnd()
    {
        buildUpTween.Kill();
        sprintTween.Kill();
        hasEnded = false;
    }

}
