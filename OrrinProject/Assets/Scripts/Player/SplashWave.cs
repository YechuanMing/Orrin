using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SplashWave : MonoBehaviour
{

    public int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {


            GameObject spr = collision.gameObject.GetComponent<SpiritualEnemyBase>().spirit;
            if (spr)
            {
                spr.SetActive(true);
                spr.GetComponent<Destructable>().Damage(1);
                DOVirtual.DelayedCall(0.1f, () => { spr.SetActive(false); });
            }

            //Destructable enm = collision.gameObject.GetComponent<Destructable>();
            //if (!enm.interactable)
            //{
            //    enm.interactable = true;
            //}
            collision.gameObject.GetComponent<Destructable>().Damage(damage);

            Debug.Log("HitEnemy");




            //spr.SetActive(false);
            
        }
    }
}
