using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        //PlayerSpiritualization.Spritualize += FollowSpirit;
        //PlayerSpiritualization.DeSpritualize += FollowBody;
        cam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static Cinemachine.CinemachineVirtualCamera cam;
    public static Transform playerBodyTrans;
    public static Transform playerSpiritTrans;

    //public void FollowBody()
    //{
    //    cam.Follow = playerBodyTrans ? playerBodyTrans : null;
    //    cam.LookAt= playerBodyTrans ? playerBodyTrans : null;
    //}

    //public void FollowSpirit()
    //{
    //    cam.Follow = playerSpiritTrans ? playerSpiritTrans : null;
    //    cam.LookAt = playerSpiritTrans ? playerSpiritTrans : null;  
    //}

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
}
