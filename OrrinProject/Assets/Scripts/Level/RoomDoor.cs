using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class RoomDoor : MonoBehaviour
{
    [Serializable]
    public struct DoorID
    {
        public LevelZone zoneTag;
        public int roomNumber;
        public int doorNumber;
    }

    public DoorID doorID;

    public DoorID linkDoorID;

    public Transform boundPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            string sceneName = linkDoorID.zoneTag.ToString() + "_" + linkDoorID.roomNumber;
            SceneChanger.Instance.TransitionToScene(sceneName, linkDoorID.doorNumber);
        }

    }

}

public enum LevelZone
{
    A, B, C, D, E, Demo
}
