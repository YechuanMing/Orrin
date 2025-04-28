using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class determineSpiritState : MonoBehaviour
{
    public GameObject spirit1;
    public GameObject spirit2;
    // Start is called before the first frame update
    public void spiritState()
    {
        if (!gameObject.GetComponent<SpiritualEnemyBase>().isSpirit_Died_1)
            spirit1.SetActive(true);
        else if (!gameObject.GetComponent<SpiritualEnemyBase>().isSpirit_Died_2)
            spirit2.SetActive(true);
        else
        {

        }
    }
}
