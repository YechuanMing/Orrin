using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        cam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Cinemachine.CinemachineVirtualCamera cam;
    public static Transform playerBodyTrans;
    public static Transform playerSpiritTrans;

    //用来在身体和灵魂之间切换镜头
    public static void SwitchFollowState(PlayerSpiritualization.SpiritState newState)
    {
        if(newState== PlayerSpiritualization.SpiritState.Spiritual)
        {
            cam.Follow = playerSpiritTrans ? playerSpiritTrans : null;
            cam.LookAt = playerSpiritTrans ? playerSpiritTrans : null;
        }else
        {
            cam.Follow = playerBodyTrans ? playerBodyTrans : null;
            cam.LookAt = playerBodyTrans ? playerBodyTrans : null;
        }
    }

    public static void Initialize(Transform body)
    {
        if(cam!=null)
        {
            cam.Follow = body;
            cam.LookAt = body;
            PlayerSpiritualization.m_State = PlayerSpiritualization.SpiritState.Physical;
            playerBodyTrans = body;
        }
    }
}
