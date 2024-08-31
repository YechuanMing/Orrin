using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Spiritualize_Enemy : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerController.Spritualize += Spritualize;
        PlayerController.DeSpritualize += DeSpiritualize;
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

    }

    public UnityEvent OnEnemySpritualized;
    public UnityEvent OnEnemyDespritualized;

}
