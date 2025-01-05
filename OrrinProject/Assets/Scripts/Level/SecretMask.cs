using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SecretMask : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerInZone;
    public bool doNotHideAgain;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
        animator.Play("fadeTo0");
    }

    private void Hide()
    {
        if (isPlayerInZone)
            return;
        animator.Play("fadeTo1");
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInZone = false;
        if(doNotHideAgain)
        {
            return;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("fadeTo1"))
        {
            return;
        }

        animator.Play("fadeTo1");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        isPlayerInZone = true;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("fadeTo0"))
        {
            return;
        }

        animator.Play("fadeTo0");
    }

}
