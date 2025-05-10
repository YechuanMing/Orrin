using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int value=1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.playerDataObj.wealth += value;
            PlayerDisplayData.Instance.UpdateMoneyNum();
            Destroy(gameObject);
        }
    }
}
