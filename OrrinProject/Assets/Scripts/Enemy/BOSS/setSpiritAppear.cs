using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setSpiritAppear : MonoBehaviour
{
    public triggerBossSpiritAnim soul1;
    public triggerBossSpiritAnim soul2;
    public triggerBossSpiritAnim soul3;
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
        if (soul1 != null)
            soul1.isTriggerAnim = false;
        if (soul2 != null)
            soul2.isTriggerAnim = false;
        if (soul3 != null)
            soul3.isTriggerAnim = false;
    }
}
