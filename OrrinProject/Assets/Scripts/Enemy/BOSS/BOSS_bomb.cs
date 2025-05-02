using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_bomb : MonoBehaviour
{
    public GameObject Bomb;
    public void bomb()
    {
        Vector3 center = transform.position;
        Instantiate(Bomb, center, Quaternion.identity);
    }
}
