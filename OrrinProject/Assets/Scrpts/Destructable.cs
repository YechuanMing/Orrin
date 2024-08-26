using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Unity;


//�˽ű����������пɱ���ҹ����ݻٵ����壬�������ڵ��ˡ�
public class Destructable : MonoBehaviour
{
    [Header("�Ƿ�Ϊ���������")]
    public bool IsSpiritual;
    [Header("��������ֵ")]
    public float currPhysicalHealth;
    public float maxPhysicalHealth;
    [Header("�������ֵ")]
    public float currSpiritualHealth;
    public float maxSpiritualHealth;

    [Header("�Ƿ��ڿɽ���״̬")]
    private bool interactable;

    [SerializeField]
    [Header("�ӳ���������")]
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
