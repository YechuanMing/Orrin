using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class SpiritualEnemyBase : MonoBehaviour
{

    public bool isSpiritDied;
    public bool isBodyDied;

    private void OnEnable()
    {
        PlayerSpiritualization.SpiritualizeBroadcast += Spritualize;
        PlayerSpiritualization.DeSpiritualizeBroadcast += DeSpiritualize;
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

    public void BodyDied()
    {
        isBodyDied = true;
    }

    public void SpiritDied()
    {
        isSpiritDied = true;
    }

    public UnityEvent OnEnemySpritualized;
    public UnityEvent OnEnemyDespritualized;

}
