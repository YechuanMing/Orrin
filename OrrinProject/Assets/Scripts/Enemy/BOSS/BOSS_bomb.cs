using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_bomb : MonoBehaviour
{
    public GameObject Bomb;
    public float radius = 5f;
    public void bomb_bodyAttack1()
    {
        Vector3 center = transform.position;
        //Instantiate(Bomb, center, Quaternion.identity);
        // ÉÏ
        Instantiate(Bomb, center + new Vector3(0, radius, 0), Quaternion.identity);

        // ÏÂ
        Instantiate(Bomb, center + new Vector3(0, -radius, 0), Quaternion.identity);

        // ×ó
        Instantiate(Bomb, center + new Vector3(-radius, 0, 0), Quaternion.identity);

        // ÓÒ
        Instantiate(Bomb, center + new Vector3(radius, 0, 0), Quaternion.identity);
    }
    public void bomb_bodyAttack2()
    {
        Vector3 center = transform.position;

        Instantiate(Bomb, center + new Vector3(-2*radius, 0, 0), Quaternion.identity);
        // ×ó
        Instantiate(Bomb, center + new Vector3(-radius, 0, 0), Quaternion.identity);
        Instantiate(Bomb, center + new Vector3(2*radius, 0, 0), Quaternion.identity);
        // ÓÒ
        Instantiate(Bomb, center + new Vector3(radius, 0, 0), Quaternion.identity);
    }
    public void bomb()
    {
        Vector3 center = transform.position;
        Instantiate(Bomb, center, Quaternion.identity);
        
    }
}
