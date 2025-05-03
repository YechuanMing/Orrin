using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSappearPlayerCon : MonoBehaviour
{
   public void PlayerConOFF()
    {
        GameObject palyer = GameObject.FindGameObjectWithTag("Player");
        palyer.GetComponent<PlayerController>().enabled = false;
    }
    public void PlayerConON()
    {
        GameObject palyer = GameObject.FindGameObjectWithTag("Player");
        palyer.GetComponent<PlayerController>().enabled = true;
    }
}
