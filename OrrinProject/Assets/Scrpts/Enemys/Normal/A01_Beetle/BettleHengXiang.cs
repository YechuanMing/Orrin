using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BettleHengXiang : MonoBehaviour
{
    
    public Transform endPoint; // ÷’µ„Œª÷√
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(endPoint.position, 10f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
