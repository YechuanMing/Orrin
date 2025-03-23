using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithCam : MonoBehaviour
{
    public Transform cameraTransform;
    [Range(-1,1)]
    public float parallaxFactor;
    private float interpolationFactor = 4f;
    public float threshold = 0.01f;

    private Vector3 currentPosition;
    private Vector3 nextPosition;
    private Vector3 previousCameraPosition;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GameObject.Find("PlayerCam").transform;/*Camera.main.transform; *//*PlayerController.Instance.transform;*/
        }
        previousCameraPosition = cameraTransform.position;
        currentPosition = transform.position;
        nextPosition = currentPosition;
    }

    void LateUpdate()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GameObject.Find("PlayerCam").transform; /*PlayerController.Instance.transform;*/
        }
        Vector3 deltaMovement = cameraTransform.position - previousCameraPosition;
        nextPosition = currentPosition + new Vector3(deltaMovement.x * parallaxFactor, deltaMovement.y * parallaxFactor, 0);

        float distance = Vector3.Distance(currentPosition, nextPosition);
        if (distance>threshold)
        {
            // 在当前位置和下一位置之间进行插值
            transform.position = Vector3.Lerp(currentPosition, nextPosition, interpolationFactor);
            currentPosition = nextPosition;
        }
        else
        {
            // 当距离小于阈值时，直接将背景移动到目标位置
            transform.position = nextPosition;
            currentPosition = nextPosition;
        }

        previousCameraPosition = cameraTransform.position;
    }
}
