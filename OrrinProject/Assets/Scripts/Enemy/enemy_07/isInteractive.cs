using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isInteractive : MonoBehaviour
{
    private Destructable destructable;
    private SpiritualEnemyBase spiritual;
    // Start is called before the first frame update
    void Start()
    {
        destructable = transform.GetComponent<Destructable>();
        spiritual = transform.GetComponent<SpiritualEnemyBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spiritual.isSpirit)
        {
            destructable.interactable = true;
        }
        else if (!spiritual.isSpirit)
        {
            destructable.interactable = false;
        }
    }
}
