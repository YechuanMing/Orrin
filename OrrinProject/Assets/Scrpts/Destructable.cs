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

    [Header("当前生命值")]
    public int currHealth;
    [Header("最大生命值")]
    public int maxHealth;


    [Header("是否处于可交互状态")]
    [SerializeField]
    protected bool interactable;

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
                    DestroyThisDelayed();
                    OnDeath.Invoke();
                }
                else
                {
                    OnDamage.Invoke();
                }
            }

        }
    }

    //延迟销毁
    protected void DestroyThisDelayed()
    {
        Debug.Log("Detroy In" + delayDestroyTime + "seconds");
        Destroy(gameObject, delayDestroyTime);
    }

    public void Damage(int damage)
    {
        CurrHealth -= damage;
        Debug.Log("Hit");
        
    }


    //受击闪烁功能。
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // 持续的变白时间
    public float flashDuration = 0.1f;
    // 变白的颜色
    public Color flashColor = Color.white;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

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
