using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplayData : MonoBehaviour
{

    private PlayerData playerDataObject;
    public static PlayerDisplayData Instance { get; private set; }

    public Slider spiritEnergyBar;

    public GameObject[] healthSprites;

    [SerializeField]
    private Destructable playerDestructable;

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

    public void SetCurrPlayerDestructable(Destructable set)
    {
        playerDestructable = set;
    }


    private void Start()
    {
        playerDataObject = GameManager.Instance.playerDataObj;
        //playerDestructable = PlayerController.Instance.gameObject.GetComponent<Destructable>();
        
    }
    private void Update()
    {
        spiritEnergyBar.value = PlayerSpiritualization.Instance.currSpiritEnergy / playerDataObject.maxSpiritAmount;

    }

    public void UpdatePlayerHealthDisplay()
    {
        //显示最大生命值范围内的生命槽，如果没显示，就显示
        //如果当前的生命槽序列超过了当前血量，就播放空槽动画
        //用i+1就可以对应血量了
        for (int i = 1; i <= healthSprites.Length-1; i++)
        {
            if(i>playerDestructable.maxHealth)
            {
                healthSprites[i].SetActive(false);
            }else
            {
                
                healthSprites[i].SetActive(true);
                
            }
            if(i>playerDestructable.CurrHealth)
            {
                healthSprites[i].GetComponent<Animator>().Play("Empty");
            }
            else
            {
                healthSprites[i].GetComponent<Animator>().Play("Filled");
            }
        }

    }
}
