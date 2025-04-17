using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidermanager : MonoBehaviour
{
     public void colliderOn()
     {
         transform.GetComponent<BoxCollider2D>().enabled = true;
     }
    public void colliderOff()
    {
        transform.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
