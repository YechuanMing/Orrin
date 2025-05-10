using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyEffectDestroy : MonoBehaviour
{

    public float destroyTime;

    public bool isAutoDestroy;

    // Start is called before the first frame update
    void Start()
    {
        if(isAutoDestroy)
        {
            Destroy(gameObject, destroyTime);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void selfDestroy()
    {
        Destroy(gameObject);
    }

    

}
