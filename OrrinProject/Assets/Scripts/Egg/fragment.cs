using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fragment : MonoBehaviour
{
    // 碎片存活时间
    public float destroyTime = 4f;

    void Start()
    {
        // 设置碎片的销毁时间
        Destroy(gameObject, Random.Range(destroyTime,destroyTime*2));
    }
}
