using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SecretMask : MonoBehaviour
{
    public SecretMaskCombine maskCombine;

    private void Start()
    {
        maskCombine = transform.parent.GetComponent<SecretMaskCombine>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        maskCombine.PlayerEntered();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        maskCombine.PlayerExited();
    }

}
