using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private  GameObject playerPref;

    public  GameObject currPlayer;

    [Header("玩家数据文件")]
    public  PlayerData playerDataObj;

    [Header("游戏的玩家起始位置")]
    public Transform firstPoint;

    [Header("最后保存坐标")]
    public Transform lastSavePoint;

    [SerializeField]
    public  int rebornTime;

    private void Awake()
    {
        // 检查是否已有实例
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 设置实例并标记为不销毁
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        SpawnPlayerAtPoint(firstPoint);
    }

    //生成玩家方法
    public  void SpawnPlayerAtPoint(Transform trans)
    {
        Debug.Log("initializePlayerAtPoint" + transform.position);
        currPlayer = Instantiate(playerPref, trans.position, trans.rotation);
        currPlayer.transform.localScale = trans.localScale;
        PlayerCameraControl.Initialize(currPlayer.transform, currPlayer.GetComponent<PlayerController>().attackFrontSpot);
    }

    public static event Action OnPlayerDynamicDataChange;
    public static event Action OnPlayerStaticDataChange;

    public void DestroyCurrPlayer()
    {
        Destroy(currPlayer);
    }
    public  void RebornPlayer()
    {
        Destroy(currPlayer, rebornTime / 2);
        SceneChanger.Instance.PlayerRebornTransition(rebornTime/2);

    }
    public void KillAllEnemyInScene()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i=0;i<allEnemies.Length;i++)
        {
            allEnemies[i].GetComponent<Destructable>().DestroyThisDelayed(0.1f);
            Debug.Log("销毁" + allEnemies[i].name);
        }
    }

}
