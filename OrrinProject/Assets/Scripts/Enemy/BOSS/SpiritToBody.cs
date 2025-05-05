using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritToBody : MonoBehaviour
{
    public SoulsBeDestroyInStage be;
    // Start is called before the first frame update
    public void ToBodyState()
    {
      
        be.Bedestroy();
        PlayerSpiritualization.Instance.DeSpiritualize();
    }
}
