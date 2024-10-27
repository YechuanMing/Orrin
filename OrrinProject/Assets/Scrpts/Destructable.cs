using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Unity;


//此脚本适用于所有可被玩家攻击摧毁的物体，不仅限于敌人。
public class Destructable : MonoBehaviour
{

    [Header("生命值")]
    public int currHealth;
    public int maxHealth;


    [Header("是否处于可交互状态")]
    [SerializeField]
    protected bool interactable;

    [SerializeField]
    [Header("延迟销毁物体")]
    [Range(0, 10)]
    protected float delayDestroyTime = 2f;

    public UnityEvent OnDeath;
    public UnityEvent OnDamage;


    public int CurrHealth
    {
        get { return currHealth; }
        set
        {
            if (interactable)
            {
                currHealth = value;

                if (currHealth <= 0)
                {
                    interactable = false;
                    DestroyThisDelayed();
                    OnDeath.Invoke();
                }
                else
                {
                    OnDamage.Invoke();
                }
            }

        }
    }

    protected void DestroyThisDelayed()
    {
        Debug.Log("Detroy In" + delayDestroyTime + "seconds");
        Destroy(gameObject, delayDestroyTime);
    }

    public void Damage(int damage)
    {
        CurrHealth -= damage;
        Debug.Log("Hit");

    }


}
