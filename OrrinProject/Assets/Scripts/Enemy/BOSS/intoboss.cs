using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intoboss : MonoBehaviour
{
    private bool isTrigger = false;
    public GameObject triggerBoss;
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
            isTrigger = true;
            triggerBoss.SetActive(true);
        }
    }
}
