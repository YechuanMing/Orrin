using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName ="ScriptaleObjects/PlayerData")]
[Serializable]
public class PlayerData : ScriptableObject 
{
    [Header("生命值")]
    public int maxHealth;

    [Header("移动")]
    
    //普通状态移动速度
    public float moveSpeed = 4.5f;
    //跳跃力度
    public float jumpForce = 7.5f;
    //跳跃最长时间
    [Range(0.5f, 1)] 
    public float jumpMaxTime = 0.7f;
    //跳跃乘数
    [Range(0.01f, 10)] 
    public float jumpStartPower = 2f;

    //灵魂状态移动速度
    public float moveSpeed_Spiritual;

    [Header("冲刺")]
    //是否获得冲刺技能
    public bool canDash;
    //冲刺冷却时间
    public float dashCoolDownTime;
    [Header("二段跳")]
    //是否获得二段跳能力
    public bool canDoubleJump;
    //二段跳跃速度
    public float doubleJumpSpeed;

    [Header("灵魂传送")]
    //是否获得
    public bool canTeleport;
    //传送消耗灵魂能量
    public int TeleportCostSpirit;


    [Header("普通攻击")]
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
    //最大灵魂值
    public int maxSpiritEnergy;
    //灵魂衰减值
    public int spiritDeclinationPerSec;
    //普通攻击获取灵魂值
    public int spiritGainPerHit;

    [Header("重生时间")]
    public int playerRebornTime;

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value;PlayerDisplayData.Instance.UpdatePlayerHealthDisplay(); }
    }

    public int MaxSpiritEnergy
    {
        get { return maxSpiritEnergy; }
        set { maxSpiritEnergy = value; PlayerDisplayData.Instance.UpdateSpiritBar(); }
    }
}
