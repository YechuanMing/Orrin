using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class Spiritualize_Enemy : MonoBehaviour
{
    //SpiritualBased的类型，是如果玩家不将灵魂状态的敌人击败，物理状态的将不会死亡，会锁血甚至会根本无血条。怪物的核心是基于灵魂模式的。
    //SpiritualBased的类型，玩家会在物理模式下依然受到灵魂攻击的伤害。
    public enum SpiritualEnemyType
    {
        PhysicalBased,SpiritualBased
    }

    public SpiritualEnemyType enemyType;

    private bool isSpiritKilled;

    private bool isBodyKilled;

    public bool IsSpiritKilled
    {
        get { return isSpiritKilled; }
        set
        {
            if (value == true)
            {
                isSpiritKilled = value;
                if(enemyType==SpiritualEnemyType.SpiritualBased)
                {
                    Destroy(this, 2f);
                }
            }
        }
    }
    public bool IsBodyKilled
    {
        get { return isBodyKilled; }
        set
        {
            if(value==true)
            {
                isBodyKilled = value;
                Destroy(this, 2f);
            }
        }
    }
    private void OnEnable()
    {
        PlayerSpiritualization.Spritualize += Spritualize;
        PlayerSpiritualization.DeSpritualize += DeSpiritualize;
    }

    private void Spritualize()
    {
        Debug.Log("切换为灵魂状态");
        OnEnemySpritualized?.Invoke();
    }

    private void DeSpiritualize()
    {
        Debug.Log("切换为生命状态");
        OnEnemyDespritualized?.Invoke();
    }

    public void SpiritKilled()
    {
        IsSpiritKilled = true;
    }

    public void BodyKilled()
    {
        IsBodyKilled = true;
    }
    

    public UnityEvent OnEnemySpritualized;
    public UnityEvent OnEnemyDespritualized;

}
