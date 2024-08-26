using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class Spiritualize_Enemy : MonoBehaviour
{
    public bool canSpritualize;
    private void OnEnable()
    {
        PlayerController.OnSpritualized += Spritualize;
    }

    private void Spritualize()
    { 

    }
}
