using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class SpiritualEnemyBase : MonoBehaviour
{

    public bool isSpirit;

    public bool isSpiritDied;
    public bool isBodyDied;
    public GameObject spirit;

    private void OnEnable()
    {
        PlayerSpiritualization.SpiritualizeBroadcast += Spritualize;
        PlayerSpiritualization.DeSpiritualizeBroadcast += DeSpiritualize;

    }

    private void Spritualize()
    {
        Debug.Log("ÇÐ»»ÎªÁé»ê×´Ì¬");
        isSpirit = true;
        OnEnemySpritualized?.Invoke();
    }

    private void DeSpiritualize()
    {
        Debug.Log("ÇÐ»»ÎªÉúÃü×´Ì¬");
        isSpirit = false;
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
