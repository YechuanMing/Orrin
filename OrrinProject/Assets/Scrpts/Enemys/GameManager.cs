using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private GameObject playerPref;

    public static GameObject player;

    private void Awake()
    {
        // ����Ƿ�����ʵ��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // ����ʵ�������Ϊ������
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public Transform firstPoint;
    private void Start()
    {
        SpawnPlayerAtPoint(firstPoint);
    }

    public void SpawnPlayerAtPoint(Transform trans)
    {
        player = Instantiate(playerPref, trans.position, trans.rotation);
        player.transform.localScale = trans.localScale;
    }
}
