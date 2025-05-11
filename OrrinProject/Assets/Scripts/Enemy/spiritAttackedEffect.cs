using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiritAttackedEffect : MonoBehaviour
{
    public GameObject enemyAttackedEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void effectsGeneration()
    {
        Vector3 center = transform.position;
        GameObject gameObject= Instantiate(enemyAttackedEffect, center, Quaternion.identity);
    }
}
