using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsBeDestroyInStage2 : MonoBehaviour
{
    public bool shock1 = false;
    public bool shock2 = false;
    public Destructable destructable;
    public int firstBeDestroyHealth;
    public int secondBeDestroyHealth;
    public int thirdBeDestroyHealth;
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
        if (destructable.currHealth <= thirdBeDestroyHealth)
        {
            transform.GetComponent<SpiritualEnemyBase>().isSpiritDied = true;
        }
        else if (destructable.currHealth <= secondBeDestroyHealth && !shock2)
        {
            shock2 = true;
        }
        else if (destructable.currHealth <= firstBeDestroyHealth && !shock1)
        {
            shock1 = true;
        }
    }
}
