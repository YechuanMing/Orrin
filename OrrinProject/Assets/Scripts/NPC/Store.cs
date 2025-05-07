using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public List<Product> products;
    public int currSelectIndex;

    private void Start()
    {
        currSelectIndex = -1;
    }
    public void Update()
    {
        Select();
        Buy();
    }


    public void Buy()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            products[currSelectIndex].Purchase();
        }

    }

    public void Select()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            if(currSelectIndex!=-1)
            {
                products[currSelectIndex].OnDeSelectEffect();
            }

            if(currSelectIndex<=0)
            {
                currSelectIndex = products.Count - 1;
            }else
            {
                currSelectIndex -= 1;
            }

            products[currSelectIndex].OnSelectEffect();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currSelectIndex != -1)
            {
                products[currSelectIndex].OnDeSelectEffect();
            }

            if (currSelectIndex >= products.Count-1)
            {
                currSelectIndex = 0;
            }
            else
            {
                currSelectIndex += 1;
            }

            products[currSelectIndex].OnSelectEffect();
        }
    }
}
