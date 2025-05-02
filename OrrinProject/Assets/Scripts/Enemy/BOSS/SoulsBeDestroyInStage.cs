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
            Animator animator = thirdSoul.GetComponent<Animator>();
            animator.Play("Die");
            Destroy(thirdSoul, 2f);
        }
        else if (destructable.currHealth <= secondBeDestroyHealth)
        {
            Animator animator = secondSoul.GetComponent<Animator>();
            animator.Play("Die");
            Destroy(secondSoul, 2f);
        }
        else if (destructable.currHealth <= firstBeDestroyHealth)
        {
            Animator animator = firstSoul.GetComponent<Animator>();
            animator.Play("Die");
            Destroy(firstSoul, 2f);
        }
    }
}
