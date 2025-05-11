using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezeRigidbody2D : MonoBehaviour
{
    private Rigidbody2D rb;

   
    public void freezePosition()
    {
        rb = gameObject. transform.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
