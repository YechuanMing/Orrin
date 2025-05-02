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
    private bool firstBeDestroy=false;
    private bool secondBeDestroy=false;
    private bool thirdBeDestroy = false;
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
        if (destructable.currHealth <= thirdBeDestroyHealth&&!thirdBeDestroy)
        {
            Animator animator = thirdSoul.GetComponent<Animator>();
            animator.Play("Die");
            thirdBeDestroy = true;
            Destroy(thirdSoul, 2f);
        }
        else if (destructable.currHealth <= secondBeDestroyHealth&&!secondBeDestroy)
        {
            Animator animator = secondSoul.GetComponent<Animator>();
            animator.Play("Die");
            secondBeDestroy = true;
            Destroy(secondSoul, 2f);
        }
        else if (destructable.currHealth <= firstBeDestroyHealth&&!firstBeDestroy)
        {
            Animator animator = firstSoul.GetComponent<Animator>();
            animator.Play("Die");
            firstBeDestroy = true;
            Destroy(firstSoul, 2f);
        }
    }
}
