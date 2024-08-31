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
        Debug.Log("�л�Ϊ���״̬");
        OnEnemySpritualized?.Invoke();
    }

    private void DeSpiritualize()
    {
        Debug.Log("�л�Ϊ����״̬");
        OnEnemyDespritualized?.Invoke();
    }

    public void SpiritKilled()
    {

    }

    public UnityEvent OnEnemySpritualized;
    public UnityEvent OnEnemyDespritualized;

}
