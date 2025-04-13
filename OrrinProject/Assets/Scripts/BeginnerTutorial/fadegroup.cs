using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class fadegroup : MonoBehaviour
{
    public float fadeDuration = 0.5f;

    public SpriteRenderer sprite;
    public TextMeshPro tmp;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(0f, 1f));
        StartCoroutine(FadeSprite(0f, 0.4f));
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(1f, 0f));
        StartCoroutine(FadeSprite(0.4f, 0f));
    }

    private IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        Color textColor = tmp.color;

        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;
            float alpha = Mathf.Lerp(from, to, t);

            if (tmp != null)
            {
                tmp.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }
        if (tmp != null)
        {
            tmp.color = new Color(textColor.r, textColor.g, textColor.b, to);
        }
    }
    private IEnumerator FadeSprite(float from, float to)
    {
        float elapsed = 0f;

        Color spriteColor = sprite.color;
       
        while (elapsed < fadeDuration)
        {
            float t = elapsed / fadeDuration;
            float alpha = Mathf.Lerp(from, to, t);

            if (sprite != null)
            {
                sprite.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            }
          
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 确保最后一帧设定到目标值
        if (sprite != null)
        {
            sprite.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, to);
        }
        if (to == 0)
        {
            gameObject.SetActive(false);
        }
       
    }
}
