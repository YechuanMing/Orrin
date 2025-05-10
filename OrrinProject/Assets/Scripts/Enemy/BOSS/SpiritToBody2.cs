using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritToBody2 : MonoBehaviour
{
    public SoulsBeDestroyInStage2 be;
    // Start is called before the first frame update
    public void ToBodyState()
    {
        be.Bedestroy();
        PlayerSpiritualization.Instance.DeSpiritualize();
    }
}
