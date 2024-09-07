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

    public static event Action Spritualize;
    public static event Action DeSpritualize;

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
}
