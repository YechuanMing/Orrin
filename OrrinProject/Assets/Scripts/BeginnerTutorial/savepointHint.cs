using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savepointHint : MonoBehaviour
{
    public GameObject savePointHint;
    public float fadeDuration = 0.5f;

    private bool playerInside = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("碰到了主角了11111");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("碰到了主角了");
            playerInside = true;
            //if (PlayerPrefs.GetInt("savepointHint") == 0)
            //{
            //    PlayerPrefs.SetInt("savepointHint", 1);
                savePointHint.SetActive(true);
                savePointHint.GetComponent<fadegroup>().FadeIn();
            //}
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("主角离开");
            playerInside = false;
            if (savePointHint.activeSelf)
                savePointHint.GetComponent<fadegroup>().FadeOut();
        }
    }

    void Update()
    {
       
    }
}
