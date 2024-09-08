using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BettleCircle : MonoBehaviour
{
    public Transform[] waypoints; // ����Ѳ�ߵ�·����
    public float patrolSpeed = 1f; // ����Ѳ�ߵ��ٶ�
    public float rotationDuration = 1f; // ��ת��������ʱ��
    // Start is called before the first frame update
    void Start()
    { 
        // ����һ���պϵ�·��
        Vector3[] patrolPath = new Vector3[waypoints.Length + 1];
        patrolPath[0] = waypoints[waypoints.Length - 1].position;
        for (int i = 0; i < waypoints.Length; i++)
        {
            patrolPath[i + 1] = waypoints[i].position;
        }

        // ����Ѳ�߶���������ÿ��·����ı�ʱ��ת
        transform.DOPath(
            patrolPath,
            patrolSpeed * patrolPath.Length, // ��������ʱ��
            PathType.Linear, // ·������
            PathMode.Sidescroller2D // ·��ģʽ������2D��Ϸͨ��ʹ��Sidescroller2D
        ).SetLoops(-1, LoopType.Restart) // ����ѭ������ΪRestart������ͷ��ʼѭ��
         .SetEase(Ease.InOutQuad)// ���û�������ΪInOutQuad���ö�����ƽ��
         .OnWaypointChange(ChangeDirection);
    }

    // �ڵ���ÿ��·����ʱ�ı䷽��
    void ChangeDirection(int i)
    {
        if (i == 0) return;
        // ˳ʱ����ת90��
        transform.Rotate(0, 0f,90f, Space.World);
        // ˳ʱ����ת90�ȣ�ʹ��ƽ������ת����
        //transform.DORotate(new Vector3(0, 0, 90), rotationDuration).SetEase(Ease.InOutQuad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
