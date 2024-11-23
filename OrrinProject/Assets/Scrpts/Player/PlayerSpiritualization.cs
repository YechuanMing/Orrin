using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class PlayerSpiritualization : MonoBehaviour
{
    public enum SpiritState
    {
        Physical, Spiritual
    }

    public SpiritState m_State = SpiritState.Physical;

    public GameObject playerSpiritPref;

    public PlayerSpiritControl playerSpirit;


    //���й��Ｐ�������״̬�л�ί��
    public static event Action Spritualize;
    public static event Action DeSpritualize;

    //��ҵ�״̬�л�
    public  UnityEvent OnCharacterSpiritualized;
    public UnityEvent OnCharacterhDeSpiritualized;
    void Start()
    {

    }

    void Update()
    {
        Spiritualize();
    }

    private void Spiritualize()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (m_State == SpiritState.Physical)
            {
                Spritualize?.Invoke();
                OnCharacterSpiritualized.Invoke();
                m_State = SpiritState.Spiritual;
            }
            else
            {
                DeSpritualize?.Invoke();
                OnCharacterhDeSpiritualized.Invoke();
               m_State = SpiritState.Physical;
            }

        }

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
