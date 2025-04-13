using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class spacejump : MonoBehaviour
{
    public GameObject spaceJump;
    public float fadeDuration = 0.5f;

    private bool playerInside = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("碰到了主角了11111");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("碰到了主角了");
            playerInside = true;
            if (PlayerPrefs.GetInt("spaceJump") == 0)
            {
                PlayerPrefs.SetInt("spaceJump", 1);
                spaceJump.SetActive(true);
                spaceJump.GetComponent<fadegroup>().FadeIn();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            Debug.Log("主角离开");
            playerInside = false;
            if(spaceJump.activeSelf)
            spaceJump.GetComponent<fadegroup>().FadeOut();
        }
    }

    void Update()
    {
       // Debug.Log("碰到了主角了");
        if (playerInside && Input.GetKeyDown(KeyCode.Space) )
        {
            if (spaceJump.activeSelf)
                spaceJump.GetComponent<fadegroup>().FadeOut();
        }
    }
}
