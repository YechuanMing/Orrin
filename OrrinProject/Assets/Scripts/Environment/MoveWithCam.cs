using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCam : MonoBehaviour
{
    // 定义一个公共的比重参数，用于控制物体跟随摄像机移动的速率
    [Range(0,1)]
    public float parallaxFactor;

    // 用于存储主摄像机的Transform组件
    private Transform cameraTransform;
    // 用于存储摄像机的上一帧位置
    private Vector3 previousCameraPosition;

    void Start()
    {
        // 获取主摄像机的Transform组件
        cameraTransform = Camera.main.transform;
        // 记录摄像机的初始位置
        previousCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        // 计算摄像机在x和y轴上的位移
        Vector3 cameraDelta = cameraTransform.position - previousCameraPosition;
        // 根据比重计算物体需要移动的位移
        Vector3 parallaxDelta = new Vector3(cameraDelta.x * parallaxFactor, cameraDelta.y * parallaxFactor, 0);

        // 更新物体的位置
        transform.position += parallaxDelta;

        // 更新摄像机的上一帧位置
        previousCameraPosition = cameraTransform.position;
    }
}
