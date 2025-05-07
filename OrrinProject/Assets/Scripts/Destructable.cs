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
    [Header("是否是Boss？")]
    public bool isBoss=false;

    public GameObject attackedEffectsPerfabs;

    [Header("当前生命值")]
    public int currHealth;
    [Header("最大生命值")]
    public int maxHealth;


    [Header("是否处于可交互状态")]
    [SerializeField]
    public bool interactable = true;

    [SerializeField]
    [Header("延迟销毁物体")]
    [Range(0, 10)]
    protected float delayDestroyTime = 2f;

    [Header("收到攻击时触发事件")]
    public UnityEvent OnDamage;

    [Header("死亡时触发事件")]
    public UnityEvent OnDeath;


    private Cinemachine.CinemachineImpulseSource impulseSource;
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
                //回血了

            }

            if (isPlayer && value > maxHealth)
            {

            }
            else
            {
                currHealth = value;
            }


            if (isPlayer)
            {
                PlayerDisplayData.Instance.UpdatePlayerHealthDisplay();
                Debug.Log("UpdatedPlayerHealth");
            }


        }
    }

    void Start()
    {
        impulseSource = GetComponent<Cinemachine.CinemachineImpulseSource>();
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

    public void player_attackedEffects()
    {
        GameObject attacked = Instantiate(attackedEffectsPerfabs, transform.position, transform.rotation);
        attacked.transform.SetParent(transform);
        attacked.transform.localScale = transform.localScale;
        Destroy(attacked, 40 / 60f);
    }
    public void UpdatePlayerMaxHealth()
    {
        if (isPlayer)
            maxHealth = GameManager.Instance.playerDataObj.maxHealth;
    }

    //延迟销毁
    public void DestroyThisDelayed(float delayTime)
    {
        //如果是玩家，触发重生方法
        if (isPlayer)
        {
            GameManager.Instance.RebornPlayer_Global();

            PostProcessManager.Instance.PlayerDieVignette();

        }

        OnDeath.Invoke();
        Debug.Log("Detroy In" + delayDestroyTime + "seconds");
        Destroy(gameObject, delayDestroyTime);

    }


    //核心方法，受击
    public void Damage(int damage, bool isEnviromentHit = false)
    {
        if (interactable == false&&!isEnviromentHit)
        {
            return;
        }
        CurrHealth -= damage;
        impulseSource.GenerateImpulse();
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
                GetComponent<Animator>().Play("Attacked");
                //这里需要禁用一下playerController，否则玩家没法被rigidbody.Addforce击退，因为playerController的移动用的是rigidbody.velocity，会覆盖掉
                PlayerController.Instance.enabled = false;
                //同时禁用状态切换，否则会打乱切换逻辑。
                PlayerSpiritualization.SetAllowTransfom(false);
                //同时开无敌帧，避免被多次打。
                interactable = false;

                Coroutine FlashBlack = StartCoroutine(FlashCoroutinePlayerDamage());
                DOVirtual.DelayedCall(0.2f, () =>
                {
                    Time.timeScale = 0.4f;  
                    //如果是陷阱伤害
                    if (isEnviromentHit)
                    {
                        
                        PostProcessManager.Instance.PlayerDieVignette();
                        StartCoroutine(SceneChanger.Instance.Fade(1, 0.5f, 1));
                        Time.timeScale = 1;
                        DOVirtual.DelayedCall(GameManager.Instance.rebornTime * 0.4f * Time.timeScale,
                            () => { PlayerController.Instance.transform.position = GameManager.Instance.lastSavePoint.position; })
                        .OnComplete(() => { DOVirtual.DelayedCall(GameManager.Instance.rebornTime * 0.1f * Time.timeScale, () => { PlayerController.Instance.enabled = true; }); });
                    }//有这么一种情况就是由于玩家位置转换的延迟过长，导致无敌时间失效，但是应该不会。。毕竟目前的无敌帧有三秒之多

                }).OnComplete(() =>
                {
                    DOVirtual.DelayedCall(0.65f, () =>
                    {
                        //恢复正常
                        Time.timeScale = 1f;
                        PlayerSpiritualization.SetAllowTransfom(true);
                        if (!isEnviromentHit)
                        { PlayerController.Instance.enabled = true; }


                    }).OnComplete(() => { DOVirtual.DelayedCall(3f, () => { interactable = true; StopCoroutine(FlashBlack); spriteRenderer.color = originalColor; }); });
                });
            }

        }
        else
        {
            //如果不是玩家，会抖动一下，加强打击感。
            transform.DOShakeScale(0.3f, 0.1f, 1, 10);
            DamageSound();
        }

    }


    

    public void Heal(int heal)
    { CurrHealth += heal; }

    //受击闪烁功能。
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // 持续的变白时间
    public float flashDuration = 0.1f;
    // 变白的颜色
    public Color flashColor = Color.white;
    public float flashFadeTime = 0.2f; // 淡出持续时间（可调整）
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
            if (isBoss)
            {
                float timer = 0f;

                while (timer < flashFadeTime)
                {
                    timer += Time.deltaTime;
                    float t = timer / flashFadeTime;
                    spriteRenderer.color = Color.Lerp(flashColor, originalColor, t);
                    yield return null;
                }

                // 确保颜色完全恢复
                spriteRenderer.color = originalColor;
            }
            else
            {
                // 恢复原色
                spriteRenderer.color = originalColor;
            }
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

    public AudioClip[] damageSounds;

    public void DamageSound()
    {
        AudioSource.PlayClipAtPoint(damageSounds[Random.Range(0, damageSounds.Length)], new Vector3(transform.position.x,transform.position.y,20f));
    }
}
