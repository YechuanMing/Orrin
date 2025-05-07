using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulsBeDestroyInStage2 : MonoBehaviour
{
    private bool firstBeDestroy = false;
    private bool secondBeDestroy = false;
    private bool thirdBeDestroy = false;
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
        if (destructable.currHealth <= thirdBeDestroyHealth && !thirdBeDestroy)
        {
            animator.Play("maskBeDestroy_3");
            transform.GetComponent<SpiritualEnemyBase>().isSpiritDied = true;
            thirdBeDestroy = true;
        }
        else if (destructable.currHealth <= secondBeDestroyHealth && !secondBeDestroy)
        {
            animator.Play("maskBeDestroy_2");
            secondBeDestroy = true;
        }
        else if (destructable.currHealth <= firstBeDestroyHealth && !firstBeDestroy)
        {
            animator.Play("maskBeDestroy_1");
            firstBeDestroy = true;
        }
    }
}
