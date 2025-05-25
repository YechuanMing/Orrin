using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int value=1;
    public AudioClip[] pickSounds;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.playerDataObj.wealth += value;
            PlayerDisplayData.Instance.UpdateMoneyNum();
            AudioManager.Instance.PlaySoundEffect(pickSounds[Random.Range(0, pickSounds.Length - 1)]);
            Destroy(gameObject);

        }
    }
}
