using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BettleCircle : MonoBehaviour
{
    public Transform[] waypoints; // 敌人巡逻的路径点
    public float patrolSpeed = 1f; // 敌人巡逻的速度
    // Start is called before the first frame update
    void Start()
    { 
        // 创建一个闭合的路径
        Vector3[] patrolPath = new Vector3[waypoints.Length + 1];
        patrolPath[0] = waypoints[waypoints.Length - 1].position;
        for (int i = 0; i < waypoints.Length; i++)
        {
            patrolPath[i + 1] = waypoints[i].position;
        }

        // 创建巡逻动画，并在每个路径点改变时旋转
        transform.DOPath(
            patrolPath,
            patrolSpeed * patrolPath.Length, // 动画持续时间
            PathType.Linear, // 路径类型
            PathMode.Sidescroller2D // 路径模式，对于2D游戏通常使用Sidescroller2D
        ).SetLoops(-1, LoopType.Restart) // 设置循环类型为Restart，即从头开始循环
         .SetEase(Ease.InOutQuad)// 设置缓动类型为InOutQuad，让动画更平滑
         .OnWaypointChange(ChangeDirection);
    }

    // 在到达每个路径点时改变方向
    void ChangeDirection(int i)
    {
        if (i == 0) return;
        // 顺时针旋转90度
        transform.Rotate(0, 0f,90f, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
