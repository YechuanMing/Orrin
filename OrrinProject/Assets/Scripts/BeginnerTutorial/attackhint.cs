using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackhint : MonoBehaviour
{
    public GameObject attackHint;
    public float fadeDuration = 0.5f;

    private bool playerInside = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("碰到了主角了11111");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("碰到了主角了");
            playerInside = true;
            if (PlayerPrefs.GetInt("attackHint") == 0)
            {
                PlayerPrefs.SetInt("attackHint", 1);
                attackHint.SetActive(true);
                attackHint.GetComponent<fadegroup>().FadeIn();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("主角离开");
            playerInside = false;
            if (attackHint.activeSelf)
                attackHint.GetComponent<fadegroup>().FadeOut();
        }
    }

    void Update()
    {
        // Debug.Log("碰到了主角了");
        if (playerInside && Input.GetMouseButtonDown(0))
        {
            if (attackHint.activeSelf)
                attackHint.GetComponent<fadegroup>().FadeOut();
        }
    }
}
