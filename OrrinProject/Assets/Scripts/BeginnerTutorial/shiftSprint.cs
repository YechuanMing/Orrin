using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiftSprint : MonoBehaviour
{
    public GameObject dashGain;
    public int flag = 0;
    public GameObject shiftsprint;
    public float fadeDuration = 0.5f;

    private bool playerInside = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("碰到了主角了11111");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("碰到了主角了");
            playerInside = true;
            //if (flag == 0 && dashGain == null)
            //{
            //    flag = 1;
            //    shiftsprint.SetActive(true);
            //    shiftsprint.GetComponent<fadegroup>().FadeIn();
            //}
            if (PlayerPrefs.GetInt("shiftSprint") == 0 && dashGain == null)
            {
                PlayerPrefs.SetInt("shiftSprint", 1);
                shiftsprint.SetActive(true);
                shiftsprint.GetComponent<fadegroup>().FadeIn();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("主角离开");
            playerInside = false;
            if (shiftsprint.activeSelf)
                shiftsprint.GetComponent<fadegroup>().FadeOut();
        }
    }

    void Update()
    {
        //if (flag == 0 && dashGain == null)
        //{
        //    flag = 1;
        //    shiftsprint.SetActive(true);
        //    shiftsprint.GetComponent<fadegroup>().FadeIn();
        //}
        if (PlayerPrefs.GetInt("shiftSprint") == 0 && dashGain == null)
        {
            PlayerPrefs.SetInt("shiftSprint", 1);
            shiftsprint.SetActive(true);
            shiftsprint.GetComponent<fadegroup>().FadeIn();
        }
    }
}
