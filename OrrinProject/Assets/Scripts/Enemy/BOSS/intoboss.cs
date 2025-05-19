using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
public class intoboss : MonoBehaviour
{
    private bool isTrigger = false;
    public GameObject triggerBoss;

    //public UnityEvent idle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&!isTrigger)
        {
            //GameObject palyer = GameObject.FindGameObjectWithTag("Player");

            //palyer.GetComponent<PlayerController>().enabled = false;
            //palyer.GetComponent<PlayerAttackControl>().enabled = false;
            //palyer.GetComponent<PlayerSpiritualization>().enabled = false;

            //idle?.Invoke();

            isTrigger = true;
            triggerBoss.SetActive(true);
            AudioManager.Instance.PlayBGM(3, true);
        }
    }
}
