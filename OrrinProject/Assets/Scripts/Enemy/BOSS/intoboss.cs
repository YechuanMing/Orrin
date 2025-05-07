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
            StartCoroutine(playerCon());
            isTrigger = true;
            triggerBoss.SetActive(true);
        }
    }
    private IEnumerator playerCon()
    {
        GameObject palyer = GameObject.FindGameObjectWithTag("Player");

        Animator animator = palyer.GetComponent<Animator>();
        animator.Play("Idle");
        yield return null; // 等一帧，确保动画生效

        palyer.GetComponent<PlayerController>().enabled = false;
        palyer.GetComponent<PlayerAttackControl>().enabled = false;
        palyer.GetComponent<PlayerSpiritualization>().enabled = false;

    }
}
