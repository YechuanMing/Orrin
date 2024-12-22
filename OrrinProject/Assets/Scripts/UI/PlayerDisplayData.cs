using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplayData : MonoBehaviour
{

    public PlayerData playerDataObject;
    public static PlayerDisplayData Instance { get; private set; }

    public Slider spiritEnergyBar;

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


    public float currSpiritEnergy;
    public float CurrSpiritEnergy
    {
        get
        {
            return currSpiritEnergy;
        }
        set
        {
            if (value <= 0)
            {
                //当玩家的灵魂能量值降到零时，自动退出灵魂模式
                if (PlayerSpiritualization.m_State == PlayerSpiritualization.SpiritState.Spiritual)
                {
                    PlayerSpiritualization.Instance.DeSpiritualize();
                }
                currSpiritEnergy = 0;
            }
            else if(value>=playerDataObject.maxSpiritAmount)
            {
                currSpiritEnergy = playerDataObject.maxSpiritAmount;
            }else
            {
                currSpiritEnergy = value;
            }
        }
    }

   
    private void Update()
    {
        spiritEnergyBar.value = currSpiritEnergy / playerDataObject.maxSpiritAmount;
        //如果当前正在灵魂状态，则自动衰减灵魂能量
        if (PlayerSpiritualization.m_State == PlayerSpiritualization.SpiritState.Spiritual)
        {
            CurrSpiritEnergy -= Time.unscaledDeltaTime * playerDataObject.spiritDeclinationPerSec;

        }
    }

    //攻击回复能量
    public void HitAddSpirit()
    {
        CurrSpiritEnergy += playerDataObject.spiritGainPerHit;
    }

}
