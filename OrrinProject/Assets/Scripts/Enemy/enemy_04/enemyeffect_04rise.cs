using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyeffect_04rise : MonoBehaviour
{
    public GameObject bombPref;
    // Start is called before the first frame update
   public void effect() { 
        
            GameObject b = GameObject.Instantiate(bombPref, new Vector3(transform.position.x, transform.position.y - 0.54f, transform.position.y), Quaternion.identity);
            Debug.Log(b);
        
       
    }
}
