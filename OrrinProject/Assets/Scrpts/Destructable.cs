using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Unity;


//此脚本适用于所有可被玩家攻击摧毁的物体，不仅限于敌人。
public class Destructable : MonoBehaviour
{

    [Header("物理生命值")]
    public float currHealth;
    public float maxHealth;


    [Header("是否处于可交互状态")]
    [SerializeField]
    protected bool interactable;

    [SerializeField]
    [Header("延迟销毁物体")]
    [Range(0, 10)]
    protected float delayDestroyTime = 2f;

    public UnityEvent OnDeath;



    public float CurrPhysicalHealth
    {
        get { return currHealth; }
        set
        {
            currHealth = value;

            if (currHealth <= 0)
            {
                interactable = false;
                OnDeath.Invoke();
            }
        }
    }

    protected void DestroyThisDelayed()
    {
        Debug.Log("Detroy In" + delayDestroyTime + "seconds");
        Destroy(gameObject, delayDestroyTime);
    }


}
