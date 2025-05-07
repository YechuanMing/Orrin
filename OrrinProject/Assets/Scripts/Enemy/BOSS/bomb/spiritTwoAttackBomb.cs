using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiritTwoAttackBomb : MonoBehaviour
{
    [Header("伤害特性")]
    [SerializeField]
    private int damage_Spr = 10;
    public Transform player;
    private Collider2D col;
    private Vector2 originalSize;
    public float scaleMultiplier = 2f;  // 放大倍数
    public float duration = 2f;         // 整个过程时间

    void Start()
    {
      
            player = PlayerSpiritControl.Instance.transform;
      
            col = GetComponent<Collider2D>();
        if (col is BoxCollider2D boxCol)
        {
            originalSize = boxCol.size;
            StartCoroutine(ScaleColliderOverTime(boxCol));
        }
        else if (col is CircleCollider2D circleCol)
        {
            originalSize = new Vector2(circleCol.radius, 0f); // 只需记录半径
            StartCoroutine(ScaleCircleCollider(circleCol));
        }
    }

    private System.Collections.IEnumerator ScaleColliderOverTime(BoxCollider2D boxCol)
    {
        float halfDuration = duration / 2f;
        float timer = 0f;

        while (timer < halfDuration)
        {
            float t = timer / halfDuration;
            boxCol.size = Vector2.Lerp(originalSize, originalSize * scaleMultiplier, t);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        while (timer < halfDuration)
        {
            float t = timer / halfDuration;
            boxCol.size = Vector2.Lerp(originalSize * scaleMultiplier, originalSize, t);
            timer += Time.deltaTime;
            yield return null;
        }

        boxCol.size = originalSize;
    }

    private System.Collections.IEnumerator ScaleCircleCollider(CircleCollider2D circleCol)
    {
        float halfDuration = duration / 2f;
        float timer = 0f;
        float originalRadius = circleCol.radius;

        while (timer < halfDuration)
        {
            float t = timer / halfDuration;
            circleCol.radius = Mathf.Lerp(originalRadius, originalRadius * scaleMultiplier, t);
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;
        while (timer < halfDuration)
        {
            float t = timer / halfDuration;
            circleCol.radius = Mathf.Lerp(originalRadius * scaleMultiplier, originalRadius, t);
            timer += Time.deltaTime;
            yield return null;
        }

        circleCol.radius = originalRadius;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerSpiritualization.Instance.DamageSpirit(damage_Spr);
        PlayerSpiritualization.Instance.DeSpiritualize();

        GameObject[] bombs = GameObject.FindGameObjectsWithTag("spiritBomb");

        foreach (GameObject bomb in bombs)
        {
            Destroy(bomb);
        }
        Destroy(this.gameObject);
    }
}
