using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Unity;


//�˽ű����������пɱ���ҹ����ݻٵ����壬�������ڵ��ˡ�
public class Destructable : MonoBehaviour
{

    [Header("����ֵ")]
    public int currHealth;
    public int maxHealth;


    [Header("�Ƿ��ڿɽ���״̬")]
    [SerializeField]
    protected bool interactable;

    [SerializeField]
    [Header("�ӳ���������")]
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
