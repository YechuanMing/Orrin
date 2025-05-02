using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactiveStele : MonoBehaviour
{
    public GameObject interactiveHint;
    public GameObject hint;
    public int flag = 0;//0为可以点击打开提示，1为可以关闭提示
    public float fadeDuration = 0.5f;

    private bool playerInside = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInside = true;
          
            interactiveHint.SetActive(true);
            interactiveHint.GetComponent<fadegroup>().FadeIn();
            
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInside = false;
            if (interactiveHint.activeSelf)
                interactiveHint.GetComponent<fadegroup>().FadeOut();
        }
    }

    void Update()
    {
        if (playerInside && Input.GetMouseButtonDown(1)&&flag==0)
        {
            if (interactiveHint.activeSelf)
                interactiveHint.GetComponent<fadegroup>().FadeOut();
            flag = 1;
            GameObject palyer= GameObject.FindGameObjectWithTag("Player");
            palyer.GetComponent<PlayerController>().enabled=false;
            gameObject.GetComponent<Animator>().Play("open");
            hint.SetActive(true);
        }
        else if(Input.GetMouseButtonDown(1) && flag == 1)
        {
            flag = 0;
            GameObject palyer = GameObject.FindGameObjectWithTag("Player");
            palyer.GetComponent<PlayerController>().enabled = true;
            gameObject.GetComponent<Animator>().Play("close");
            hint.SetActive(false);
        }
    }
}
