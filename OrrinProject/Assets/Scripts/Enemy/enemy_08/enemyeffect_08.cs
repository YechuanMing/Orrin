using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyeffect_08 : MonoBehaviour
{
    public GameObject bombPref;
    public void bombEffect()
    {
        StartCoroutine(BombRoutine());
       
    }
    private IEnumerator BombRoutine()
    {
        Vector3 center = transform.position;

        Instantiate(bombPref, center, Quaternion.Euler(0, 0, 45));
        Instantiate(bombPref, center, Quaternion.Euler(0, 0, 135));

        yield return new WaitForSeconds(0.5f);

        Instantiate(bombPref, center, Quaternion.Euler(0, 0, 90));
        Instantiate(bombPref, center, Quaternion.Euler(0, 0, 180));
    }
}
