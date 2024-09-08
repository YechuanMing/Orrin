using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : ScriptableObject 
{
    [Header("生命值")]
    public int maxHealth;
    public int currHealth;

    [Header("移动")]
    //普通状态移动速度
    public float moveSpeed_Physical;
    //灵魂状态移动速度
    public float moveSpeed_Spiritual;
    //跳跃速度
    public float jumpVelocity;
    //跳跃最长时间
    public float jumpMaxTime;

    //是否获得冲刺技能
    public bool canDash;
    //冲刺冷却时间
    public float dashCoolDownTime;

    //是否获得二段跳能力
    public bool canDoubleJump;
    //二段跳跃速度
    public float doubleJumpSpeed;

    [Header("攻击")]
    //普通攻击力
    public int ATK_Physical;
    //普通攻击击退效果
    public float repelForce_Physical;
    //普通攻击攻速
    public float attackCoolDown_Physical;

    //灵魂攻击力
    public int ATK_Spiritual;
    //灵魂攻击的冲刺力度
    public float attackDashForce_Spiritual;
    //灵魂攻击的攻击速度
    public float attackCoolDown_Spiritual;

    [Header("灵魂化相关 ")]
    //当前
    public float currSpiritAmount;
    public float maxSpiritAmount;
    public float spiritAttenuationPerSec;
    public float spiritGainPerHit;


}
