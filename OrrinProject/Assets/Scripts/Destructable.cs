using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Unity;
using DG.Tweening;


//此脚本适用于所有可被玩家攻击摧毁的物体，不仅限于敌人。
public class Destructable : MonoBehaviour
{
    [Header("是否是玩家？")]
    public bool isPlayer;
    [Header("当前生命值")]
    public int currHealth;
    [Header("最大生命值")]
    public int maxHealth;


    [Header("是否处于可交互状态")]
    [SerializeField]
    protected bool interactable = true;

    [SerializeField]
    [Header("延迟销毁物体")]
    [Range(0, 10)]
    protected float delayDestroyTime = 2f;

    [Header("收到攻击时触发事件")]
    public UnityEvent OnDamage;

    [Header("死亡时触发事件")]
    public UnityEvent OnDeath;




    public int CurrHealth
    {
        get { return currHealth; }
        set
        {

            if (value < currHealth)
            {
                if (value <= 0)
                {
                    interactable = false;
                    DestroyThisDelayed(delayDestroyTime);
                }
                OnDamage.Invoke();

            }
            if (value > currHealth)
            {

            }
            currHealth = value;

            if (isPlayer)
            {
                PlayerDisplayData.Instance.UpdatePlayerHealthDisplay();
                Debug.Log("UpdatedPlayerHealth");
            }


        }
    }

    void Start()
    {

        //如果是玩家，从数据中获取生命值。（感觉不太对，要改）
        if (isPlayer && GameManager.Instance.playerDataObj != null)
        {
            maxHealth = GameManager.Instance.playerDataObj.maxHealth;
            PlayerDisplayData.Instance.SetCurrPlayerDestructable(this);

            //PlayerDisplayData.Instance.UpdatePlayerHealthDisplay();
            Debug.Log("Hi");
        }

        //刷新生命值
        CurrHealth = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }


    }

    public void UpdateMaxHealth()
    {
        maxHealth = GameManager.Instance.playerDataObj.maxHealth;
    }

    //延迟销毁
    public void DestroyThisDelayed(float delayTime)
    {
        //如果是玩家，触发重生方法
        if (isPlayer)
        {
            GameManager.Instance.RebornPlayer();
            PostProcessManager.Instance.PlayerDieVignette();

        }

        OnDeath.Invoke();
        Debug.Log("Detroy In" + delayDestroyTime + "seconds");
        Destroy(gameObject, delayDestroyTime);

    }


    //核心方法，受击
    public void Damage(int damage, bool isEnviromentHit = false)
    {
        if (interactable == false)
        {
            return;
        }
        CurrHealth -= damage;
        Debug.Log("HitBy" + damage);
        if (CurrHealth <= 0)
        {
            return;
        }
        //如果目前挂这个物体的是玩家，需要有独特的受击处理
        if (isPlayer && currHealth > 0)
        {
            //如果玩家处于灵魂状态，弹出来。
            if (PlayerSpiritualization.m_State == PlayerSpiritualization.SpiritState.Spiritual)
            {
                PlayerSpiritualization.Instance.DeSpiritualize();
            }

            //if (currHealth > 0)
            {
                //以下是玩家受攻击时候的僵硬和击退等效果
                //短暂慢镜头
                //Time.timeScale = 0.2f;
                PostProcessManager.Instance.PlayerDamagedVignette();
                //这里需要禁用一下playerController，否则玩家没法被rigidbody.Addforce击退，因为playerController的移动用的是rigidbody.velocity，会覆盖掉
                PlayerController.Instance.enabled = false;
                //同时禁用状态切换，否则会打乱切换逻辑。
                PlayerSpiritualization.SetAllowTransfom(false);
                //同时开无敌帧，避免被多次打。
                interactable = false;

                Coroutine FlashBlack = StartCoroutine(FlashCoroutinePlayerDamage());
                DOVirtual.DelayedCall(0.2f, () =>
                {
                Time.timeScale = 0.2f;
                if (isEnviromentHit)
                {
                    PostProcessManager.Instance.PlayerDieVignette();
                    StartCoroutine(SceneChanger.Instance.Fade(1, 0.5f, 1));
                        Time.timeScale = 1;
                        DOVirtual.DelayedCall(GameManager.Instance.rebornTime * 0.5f * Time.timeScale,
                            () => { PlayerController.Instance.transform.position = GameManager.Instance.lastSavePoint.position; });
                    }//有这么一种情况就是由于玩家位置转换的延迟过长，导致无敌时间失效，但是应该不会。。毕竟目前的无敌帧有三秒之多

                }).OnComplete(() =>
                {
                    DOVirtual.DelayedCall(0.5f, () =>
                    {
                        //恢复正常
                        Time.timeScale = 1f;
                        PlayerSpiritualization.SetAllowTransfom(true);
                        PlayerController.Instance.enabled = true;

                    }).OnComplete(() => { DOVirtual.DelayedCall(3f, () => { interactable = true; StopCoroutine(FlashBlack); spriteRenderer.color = originalColor; }); });
                });
            }

        }
        else
        {
            //如果不是玩家，会抖动一下，加强打击感。
            transform.DOShakeScale(0.3f, 0.5f, 1, 30);
        }

    }


    //受击闪烁功能。
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // 持续的变白时间
    public float flashDuration = 0.1f;
    // 变白的颜色
    public Color flashColor = Color.white;
    public Color playerDamageColor = Color.black;
    public float playerDamageflashDuration = 0.3f;


    // 这个方法可以在角色被攻击时调用
    public void FlashWhite()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        if (spriteRenderer != null)
        {
            // 变白
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            // 恢复原色
            spriteRenderer.color = originalColor;
        }
    }

    public void FlashBlackPlayerDamage()
    {
        StartCoroutine(FlashCoroutinePlayerDamage());
    }

    private IEnumerator FlashCoroutinePlayerDamage()
    {
        if (spriteRenderer != null)
        {
            while (true)
            {
                spriteRenderer.color = playerDamageColor;
                yield return new WaitForSeconds(playerDamageflashDuration);
                // 恢复原色
                spriteRenderer.color = originalColor;
                yield return new WaitForSeconds(playerDamageflashDuration);
            }

        }
    }
}
