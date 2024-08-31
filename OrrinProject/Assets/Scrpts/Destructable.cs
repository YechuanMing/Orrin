using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Unity;


//此脚本适用于所有可被玩家攻击摧毁的物体，不仅限于敌人。
public class Destructable : MonoBehaviour
{
    [Header("是否为纯灵魂生物")]
    public bool IsSpiritual;
    [Header("物理生命值")]
    public float currPhysicalHealth;
    public float maxPhysicalHealth;
    [Header("灵魂生命值")]
    public float currSpiritualHealth;
    public float maxSpiritualHealth;

    [Header("是否处于可交互状态")]
    private bool interactable;

    [SerializeField]
    [Header("延迟销毁物体")]
    [Range(0, 10)]
    private float delayDestroyTime = 2f;

    public UnityEvent OnDeath;


    public event UnityAction OnSubjectDead;
    public event UnityAction OnSpiritDead;

    private void OnEnable()
    {
        OnSubjectDead += DestroyThisDelayed;
    }

    public float CurrPhysicalHealth
    {
        get { return currPhysicalHealth; }
        set
        {
            currPhysicalHealth = value;

            if (currPhysicalHealth <= 0 && !IsSpiritual)
            {
                interactable = false;
                OnSubjectDead?.Invoke();
            }
        }

    }

    public float CurrSpiritualHealth
    {
        get { 
            return currSpiritualHealth; }
        set {
            currSpiritualHealth = value;
            if(currSpiritualHealth<=0)
            {
                if(IsSpiritual)
                {
                    OnSubjectDead?.Invoke();
                }else
                {
                    OnSpiritDead?.Invoke();
                }
            }
        }
    }

    private void DestroyThisDelayed()
    {
        Debug.Log("Detroy In" + delayDestroyTime + "seconds");
        Destroy(gameObject, delayDestroyTime);
    }


}
