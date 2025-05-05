using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsBeDestroyInStage : MonoBehaviour
{
    public int BeDestroyNum = 0;
    public bool shocked1=false;
    public bool shocked2 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Bedestroy()
    {
        BeDestroyNum++;
        if (BeDestroyNum == 1)
        {
            shocked1 = true;
        }
        if (BeDestroyNum == 2)
        {
            shocked2 = true;
        }
        if (BeDestroyNum == 3)
        {
            transform.GetComponent<SpiritualEnemyBase>().isSpiritDied = true;
        }
    }
}
