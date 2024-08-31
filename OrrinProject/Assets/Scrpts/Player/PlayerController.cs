using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public static event Action OnSpritualized;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            OnSpritualized?.Invoke();
        }
    }
}
