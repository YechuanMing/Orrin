using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.DOScale(1.5f, 0.5f).SetEase(Ease.InOutBounce).OnComplete(() => { Destroy(this.gameObject); });
        }

    }
}
