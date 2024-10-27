using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Unity;


//�˽ű����������пɱ���ҹ����ݻٵ����壬�������ڵ��ˡ�
public class Destructable : MonoBehaviour
{

    [Header("��������ֵ")]
    public float currHealth;
    public float maxHealth;


    [Header("�Ƿ��ڿɽ���״̬")]
    [SerializeField]
    protected bool interactable;

    [SerializeField]
    [Header("�ӳ���������")]
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
