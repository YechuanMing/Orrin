using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Unity;
using DG.Tweening;


//�˽ű����������пɱ���ҹ����ݻٵ����壬�������ڵ��ˡ�
public class Destructable : MonoBehaviour
{

    [Header("��ǰ����ֵ")]
    public int currHealth;
    [Header("�������ֵ")]
    public int maxHealth;


    [Header("�Ƿ��ڿɽ���״̬")]
    [SerializeField]
    protected bool interactable;

    [SerializeField]
    [Header("�ӳ���������")]
    [Range(0, 10)]
    protected float delayDestroyTime = 2f;

    [Header("�յ�����ʱ�����¼�")]
    public UnityEvent OnDamage;

    [Header("����ʱ�����¼�")]
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

    //�ӳ�����
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


    //�ܻ���˸���ܡ�
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // �����ı��ʱ��
    public float flashDuration = 0.1f;
    // ��׵���ɫ
    public Color flashColor = Color.white;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    // ������������ڽ�ɫ������ʱ����
    public void FlashWhite()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        if (spriteRenderer != null)
        {
            // ���
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            // �ָ�ԭɫ
            spriteRenderer.color = originalColor;
        }
    }
}
