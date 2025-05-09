using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsBeDestroyInStage2 : MonoBehaviour
{
    private bool shock1 = false;
    private bool shock2 = false;

    public Animator animator;
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
