using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setSpirit2Appear : MonoBehaviour
{
    public triggerBossSpiritAnim soul;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void spiritAppear()
    {
        if (soul != null)
            soul.isTriggerAnim = false;
    }
}
