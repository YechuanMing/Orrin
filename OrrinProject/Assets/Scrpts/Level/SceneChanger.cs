using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance { get; private set; }


    [Header("��Ļ����")]
    [SerializeField] private CanvasGroup blackScreenCanvasGroup; // ���� CanvasGroup
    [SerializeField] private float fadeDuration = 1f; // ����/����ʱ��

    private void Awake()
    {
        // ����ģʽ
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // ��ʼ����Ļ
        if (blackScreenCanvasGroup == null)
        {
            CreateBlackScreen();
        }
        blackScreenCanvasGroup.alpha = 0; // ��ĻĬ��͸��
    }



    /// <summary>
    /// �л���ָ�����������к�������Ч��
    /// </summary>
    /// <param name="sceneName">Ŀ�곡������</param>
    public void TransitionToScene(string sceneName,int doorIndex)
    {
        StartCoroutine(PerformSceneTransition(sceneName,doorIndex));
    }

    /// <summary>
    /// ִ�г����л���Э��
    /// </summary>
    /// <param name="sceneName">Ŀ�곡������</param>
    private System.Collections.IEnumerator PerformSceneTransition(string sceneName,int doorIndex)
    {
        // ��������
        yield return FadeToBlack();

        // �л�����
        SceneManager.LoadScene(sceneName);

        // �ȴ�һ֡��ȷ�������������
        yield return null;

        SpawnPlayerNearTargetDoor(doorIndex);

        // ��������
        yield return FadeToTransparent();
    }

    /// <summary>
    /// ��������
    /// </summary>
    private System.Collections.IEnumerator FadeToBlack()
    {
        blackScreenCanvasGroup.DOFade(1, fadeDuration).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(fadeDuration);
    }

    /// <summary>
    /// ��������
    /// </summary>
    private System.Collections.IEnumerator FadeToTransparent()
    {
        blackScreenCanvasGroup.DOFade(0, fadeDuration).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(fadeDuration);
    }

    /// <summary>
    /// ��̬��������Ļ��
    /// </summary>
    private void CreateBlackScreen()
    {
        GameObject canvasObject = new GameObject("BlackScreenCanvas");
        DontDestroyOnLoad(canvasObject); // ȷ���糡������
        canvasObject.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasObject.AddComponent<GraphicRaycaster>();

        GameObject blackScreenObject = new GameObject("BlackScreen");
        blackScreenObject.transform.SetParent(canvasObject.transform, false);

        Image blackScreenImage = blackScreenObject.AddComponent<Image>();
        blackScreenImage.color = Color.black;

        RectTransform rectTransform = blackScreenObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;

        blackScreenCanvasGroup = blackScreenObject.AddComponent<CanvasGroup>();
    }


    /// <summary>
    /// �ڳ������ҵ�����λ�ã��������
    /// </summary>
    public void SpawnPlayerNearTargetDoor(int doorIndex)
    {
        RoomDoor targetDoor=GameObject.Find("Door_" + doorIndex.ToString()).GetComponent<RoomDoor>();
        GameManager.Instance.SpawnPlayerAtPoint(targetDoor.boundPoint);
    }


}
