using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretMaskCombine : MonoBehaviour
{
    public Animator[] animators;
    private bool isPlayerInZone;
    public bool doNotHideAgain;

    public int currEnteredMasksCount;

    private void Start()
    {
        animators = GetComponentsInChildren<Animator>();
        PlayerSpiritualization.SpiritualizeBroadcast += Expose;
        PlayerSpiritualization.DeSpiritualizeBroadcast += Hide;
    }

    private void OnDestroy()
    {
        PlayerSpiritualization.SpiritualizeBroadcast -= Expose;
        PlayerSpiritualization.DeSpiritualizeBroadcast -= Hide;
    }

    private void Expose()
    {
        if (isPlayerInZone)
            return;

        foreach (Animator animator in animators)
        {
            animator.Play("fadeTo0");
        }

    }

    private void Hide()
    {
        if (isPlayerInZone)
            return;
        foreach (Animator animator in animators)
        {
            animator.Play("fadeTo1");
        }
    }

    public void PlayerExited()
    {
        currEnteredMasksCount -= 1;

        if(currEnteredMasksCount==0)//当且仅当玩家离开所有区域之后，再触发全体的动画
        {
            isPlayerInZone = false;
            if (doNotHideAgain)
            {
                return;
            }
            //if (animators[0].GetCurrentAnimatorStateInfo(0).IsName("fadeTo1"))
            //{
            //    return;
            //}

            foreach (Animator animator in animators)
            {
                animator.Play("fadeTo1");
            }
        }


    }

    public void PlayerEntered()
    {
        currEnteredMasksCount += 1;

        if(currEnteredMasksCount==1)
        {
            isPlayerInZone = true;
            //if (animators[0].GetCurrentAnimatorStateInfo(0).IsName("fadeTo0"))
            //{
            //    return;
            //}

            foreach (Animator animator in animators)
            {
                animator.Play("fadeTo0");
            }
        }

    }
}
