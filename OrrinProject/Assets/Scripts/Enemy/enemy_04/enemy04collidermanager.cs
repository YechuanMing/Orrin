using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy04collidermanager : MonoBehaviour
{
    public BoxCollider2D colliderSmall;
    public BoxCollider2D colliderBig;
    public void collisionSmaller()
    {
        colliderBig.enabled = false;
        colliderSmall.enabled = true;
    }
    public void collisionBigger()
    {
        colliderBig.enabled = true;
        colliderSmall.enabled = false;
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
