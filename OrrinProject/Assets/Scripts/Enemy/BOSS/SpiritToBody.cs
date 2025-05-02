using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritToBody : MonoBehaviour
{
    // Start is called before the first frame update
    public void ToBodyState()
    {
        PlayerSpiritualization.Instance.DeSpiritualize();
    }
}
