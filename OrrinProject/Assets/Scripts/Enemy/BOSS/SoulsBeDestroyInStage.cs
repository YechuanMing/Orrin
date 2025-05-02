using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsBeDestroyInStage : MonoBehaviour
{
    public Destructable destructable;
    public int firstBeDestroyHealth;
    public int secondBeDestroyHealth;
    public int thirdBeDestroyHealth;
    public GameObject firstSoul;
    public GameObject secondSoul;
    public GameObject thirdSoul;
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
            Destroy(thirdSoul, 1f);
        }
        else if (destructable.currHealth <= secondBeDestroyHealth)
        {
            Destroy(secondSoul, 1f);
        }
        else if (destructable.currHealth <= firstBeDestroyHealth)
        {
            Destroy(firstSoul, 1f);
        }
    }
}
