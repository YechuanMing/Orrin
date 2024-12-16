using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class Spiritualize_Enemy : MonoBehaviour
{
    //SpiritualBased�����ͣ��������Ҳ������״̬�ĵ��˻��ܣ�����״̬�Ľ���������������Ѫ�����������Ѫ��������ĺ����ǻ������ģʽ�ġ�
    //SpiritualBased�����ͣ���һ�������ģʽ����Ȼ�ܵ���깥�����˺���
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
        IsSpiritKilled = true;
    }

    public void BodyKilled()
    {
        IsBodyKilled = true;
    }
    

    public UnityEvent OnEnemySpritualized;
    public UnityEvent OnEnemyDespritualized;

}
