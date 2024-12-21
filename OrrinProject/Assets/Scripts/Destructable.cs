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
    protected bool interactable=true;

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
            if (interactable)
            {
                currHealth = value;

                if (currHealth <= 0)
                {
                    interactable = false;
                    DestroyThisDelayed(delayDestroyTime);

                }
                else
                {
                    OnDamage.Invoke();
                }
            }

        }
    }

    void Start()
    {

        if (isPlayer && GameManager.Instance.playerDataObj != null)
        {
            maxHealth = GameManager.Instance.playerDataObj.maxHealth;
            Debug.Log("Hi");
        }

        CurrHealth = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }


    }


    //延迟销毁
    public void DestroyThisDelayed(float delayTime)
    {
        if (isPlayer)
        {
            GameManager.Instance.RebornPlayer();
        }
        OnDeath.Invoke();
        Debug.Log("Detroy In" + delayDestroyTime + "seconds");
        Destroy(gameObject, delayDestroyTime);

    }

    public void Damage(int damage)
    {
        CurrHealth -= damage;

        if (isPlayer && currHealth > 0) 
        {
            if(PlayerSpiritualization.m_State==PlayerSpiritualization.SpiritState.Spiritual)
            {
                PlayerSpiritualization.Instance.DeSpiritualize();
            }
            Time.timeScale = 0.2f;
            PostProcessManager.Instance.PlayerDamagedVignette();
            GetComponent<PlayerController>().enabled = false;
            DOVirtual.DelayedCall(0.2f, () => { Time.timeScale = 0.2f; }).OnComplete(()=>
            { DOVirtual.DelayedCall(0.5f, () => { Time.timeScale = 1f; GetComponent<PlayerController>().enabled = true; }); });
        }
        Debug.Log("HitBy"+damage);
    }


    //受击闪烁功能。
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // 持续的变白时间
    public float flashDuration = 0.1f;
    // 变白的颜色
    public Color flashColor = Color.white;



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
}
