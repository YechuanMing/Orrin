using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class PlayerSpiritualization : MonoBehaviour
{
    public static PlayerSpiritualization Instance { get; private set; }

    public static bool allowTransform=true;
    public enum SpiritState
    {
        Physical, Spiritual
    }

    public static SpiritState m_State = SpiritState.Physical;

    public GameObject playerSpiritPref;

    public PlayerSpiritControl playerSpirit;


    //所有怪物及交互物的状态切换委托
    public static event Action SpiritualizeBroadcast;
    public static event Action DeSpiritualizeBroadcast;

    //玩家的状态切换
    public UnityEvent OnCharacterSpiritualized;
    public UnityEvent OnCharacterhDeSpiritualized;

    private void Awake()
    {
        // 检查是否已有实例
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 设置实例并标记为不销毁
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {

    }

    public static void SetAllowTransfom(bool set)
    {
        allowTransform = set;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)&&allowTransform)
        {
            if (m_State == SpiritState.Physical)
            {
                Spiritualize();
            }
            else
            {
                DeSpiritualize();
            }

        }

    }

    //去灵魂化
    public  void DeSpiritualize()
    {
        DeSpiritualizeBroadcast?.Invoke();
        OnCharacterhDeSpiritualized.Invoke();
        m_State = SpiritState.Physical;
        PostProcessManager.Instance.ResetToDefault();
        Time.timeScale = 1f;
    }

    //灵魂化
    public  void Spiritualize()
    {
        //如果当前灵魂能量不足，不转变
        if (PlayerDisplayData.Instance.currSpiritEnergy <= Mathf.Epsilon)
        {
            return;
        }

        PostProcessManager.Instance.PlayerSpiritualizeCombo();
        SpiritualizeBroadcast?.Invoke();
        OnCharacterSpiritualized.Invoke();
        m_State = SpiritState.Spiritual;
        Time.timeScale = 0.5f;
    }

    
    
    //放出灵魂
    public void InstantiateSpirit()
    {
        playerSpirit = Instantiate(playerSpiritPref, transform.position, transform.rotation).GetComponent<PlayerSpiritControl>();
        PlayerCameraControl.playerSpiritTrans = playerSpirit.transform;
        PlayerCameraControl.SwitchFollowState(SpiritState.Spiritual);
    }

    //收回灵魂
    public void WithdrawSpirit()
    {
        Destroy(playerSpirit.gameObject);
        PlayerCameraControl.SwitchFollowState(SpiritState.Physical);
    }

    
}
