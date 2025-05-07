using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiritToBodyBombDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void bombDestroy()
    {
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("spiritBomb");

        foreach (GameObject bomb in bombs)
        {
            Destroy(bomb);
        }
    }
}
