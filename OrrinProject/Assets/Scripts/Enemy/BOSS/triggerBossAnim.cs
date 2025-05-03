using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerBossAnim : MonoBehaviour
{
    public bool isTriggerAnim=false;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if (!isTriggerAnim)
        {
            animator.Play("appear");
            isTriggerAnim = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
