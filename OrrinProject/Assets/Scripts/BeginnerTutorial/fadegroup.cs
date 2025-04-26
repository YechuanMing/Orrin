using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class fadegroup : MonoBehaviour
{
    public float fadeDuration = 0.5f;

    public TextMeshPro tmp;
    public Animator kuangAnimator;
    private int flag=0;//ÊÇ·ñÊÇ½¥Òþ£¬½¥ÒþºósetActive false
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void FadeIn()
    {
        flag = 0;
        StopAllCoroutines();
        StartCoroutine(Fade(0f, 1f));
        kuangAnimator.Play("appear");
    }

    public void FadeOut()
    {
        flag = 1;
        StopAllCoroutines();
        StartCoroutine(Fade(1f, 0f));
        kuangAnimator.Play("disappear");
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
        if (flag == 1)
        {
            gameObject.SetActive(false);
        }
    }
}
