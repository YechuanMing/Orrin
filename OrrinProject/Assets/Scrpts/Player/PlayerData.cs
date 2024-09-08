using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : ScriptableObject 
{
    [Header("����ֵ")]
    public int maxHealth;
    public int currHealth;

    [Header("�ƶ�")]
    //��ͨ״̬�ƶ��ٶ�
    public float moveSpeed_Physical;
    //���״̬�ƶ��ٶ�
    public float moveSpeed_Spiritual;
    //��Ծ�ٶ�
    public float jumpVelocity;
    //��Ծ�ʱ��
    public float jumpMaxTime;

    //�Ƿ��ó�̼���
    public bool canDash;
    //�����ȴʱ��
    public float dashCoolDownTime;

    //�Ƿ��ö���������
    public bool canDoubleJump;
    //������Ծ�ٶ�
    public float doubleJumpSpeed;

    [Header("����")]
    //��ͨ������
    public int ATK_Physical;
    //��ͨ��������Ч��
    public float repelForce_Physical;
    //��ͨ��������
    public float attackCoolDown_Physical;

    //��깥����
    public int ATK_Spiritual;
    //��깥���ĳ������
    public float attackDashForce_Spiritual;
    //��깥���Ĺ����ٶ�
    public float attackCoolDown_Spiritual;

    [Header("��껯��� ")]
    //��ǰ
    public float currSpiritAmount;
    public float maxSpiritAmount;
    public float spiritAttenuationPerSec;
    public float spiritGainPerHit;


}
