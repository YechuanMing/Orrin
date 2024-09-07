using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class Spiritualize_Enemy : MonoBehaviour
{
    public bool isSpiritKilled;
    private void OnEnable()
    {
        PlayerSpiritualization.Spritualize += Spritualize;
        PlayerSpiritualization.DeSpritualize += DeSpiritualize;
    }

    private void Spritualize()
    {
        Debug.Log("ÇÐ»»ÎªÁé»ê×´Ì¬");
        OnEnemySpritualized?.Invoke();
    }

    private void DeSpiritualize()
    {
        Debug.Log("ÇÐ»»ÎªÉúÃü×´Ì¬");
        OnEnemyDespritualized?.Invoke();
    }

    public void SpiritKilled()
    {
        isSpiritKilled = true;
    }

    public UnityEvent OnEnemySpritualized;
    public UnityEvent OnEnemyDespritualized;

}
