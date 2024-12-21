using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class PlayerSpiritualization : MonoBehaviour
{
    public static PlayerSpiritualization Instance { get; private set; }

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



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
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
    public  void DeSpiritualize()
    {
        DeSpiritualizeBroadcast?.Invoke();
        OnCharacterhDeSpiritualized.Invoke();
        m_State = SpiritState.Physical;
        Time.timeScale = 1f;
    }

    public  void Spiritualize()
    {
        if (PlayerDisplayData.Instance.currSpiritEnergy <= Mathf.Epsilon)
        {
            return;
        }
        
        SpiritualizeBroadcast?.Invoke();
        OnCharacterSpiritualized.Invoke();
        m_State = SpiritState.Spiritual;
        Time.timeScale = 0.5f;
    }


    public void InstantiateSpirit()
    {
        playerSpirit = Instantiate(playerSpiritPref, transform.position, transform.rotation).GetComponent<PlayerSpiritControl>();
        PlayerCameraControl.playerSpiritTrans = playerSpirit.transform;
        PlayerCameraControl.SwitchFollowState(SpiritState.Spiritual);
    }

    public void WithdrawSpirit()
    {
        Destroy(playerSpirit.gameObject);
        PlayerCameraControl.SwitchFollowState(SpiritState.Physical);
    }

    
}
