using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public List<Product> products;
    public int currSelectIndex;

    public AudioClip interactSound;

    private void Start()
    {
        currSelectIndex = -1;
       
    }

    private void OnEnable()
    {
        GetComponentInParent<DialogueNPC>().SwitchDialogueIndex(3);
    }
    public void Update()
    {
        Select();
        if(Input.GetKeyDown(KeyCode.E)&&currSelectIndex>=0&&currSelectIndex<=products.Count-1)
        {
            Buy();
        }
    }


    public void Buy()
    {
        bool result=products[currSelectIndex].Purchase();
        AudioManager.Instance.PlaySoundEffect(interactSound);

        if(result)
        {
            GetComponentInParent<DialogueNPC>().SwitchDialogueIndex(2);
            Debug.Log("PurchaseSuccess");
        }
        else
        {
            Debug.Log("PurchaseFailed");
        }
        

    }

    public void Select()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            if(currSelectIndex!=-1/*&&!products[currSelectIndex].soldOut*/)
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

            //if(!products[currSelectIndex].soldOut)
            {
                products[currSelectIndex].OnSelectEffect();
            }

        }
        if (Input.GetKeyDown(KeyCode.S) )
        {
            if (currSelectIndex != -1 /*&& !products[currSelectIndex].soldOut*/)
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
            //if (!products[currSelectIndex].soldOut)
            {
                products[currSelectIndex].OnSelectEffect();
            }

        }
    }

    private void OnDisable()
    {
        GetComponentInParent<DialogueNPC>().InitializeDialogue();
    }
}
