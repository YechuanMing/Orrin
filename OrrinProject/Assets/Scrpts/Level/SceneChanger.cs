using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance { get; private set; }


    [Header("黑幕设置")]
    [SerializeField] private CanvasGroup blackScreenCanvasGroup; // 黑屏 CanvasGroup
    [SerializeField] private float fadeDuration = 1f; // 淡入/淡出时间

    private void Awake()
    {
        // 单例模式
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 初始化黑幕
        if (blackScreenCanvasGroup == null)
        {
            CreateBlackScreen();
        }
        blackScreenCanvasGroup.alpha = 0; // 黑幕默认透明
    }



    /// <summary>
    /// 切换到指定场景并带有黑屏过渡效果
    /// </summary>
    /// <param name="sceneName">目标场景名称</param>
    public void TransitionToScene(string sceneName,int doorIndex)
    {
        StartCoroutine(PerformSceneTransition(sceneName,doorIndex));
    }

    /// <summary>
    /// 执行场景切换的协程
    /// </summary>
    /// <param name="sceneName">目标场景名称</param>
    private System.Collections.IEnumerator PerformSceneTransition(string sceneName,int doorIndex)
    {
        // 黑屏淡入
        yield return FadeToBlack();

        // 切换场景
        SceneManager.LoadScene(sceneName);

        // 等待一帧以确保场景加载完成
        yield return null;

        SpawnPlayerNearTargetDoor(doorIndex);

        // 黑屏淡出
        yield return FadeToTransparent();
    }

    /// <summary>
    /// 黑屏淡入
    /// </summary>
    private System.Collections.IEnumerator FadeToBlack()
    {
        blackScreenCanvasGroup.DOFade(1, fadeDuration).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(fadeDuration);
    }

    /// <summary>
    /// 黑屏淡出
    /// </summary>
    private System.Collections.IEnumerator FadeToTransparent()
    {
        blackScreenCanvasGroup.DOFade(0, fadeDuration).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(fadeDuration);
    }

    /// <summary>
    /// 动态创建黑屏幕布
    /// </summary>
    private void CreateBlackScreen()
    {
        GameObject canvasObject = new GameObject("BlackScreenCanvas");
        DontDestroyOnLoad(canvasObject); // 确保跨场景存在
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
    /// 在场景中找到传送位置，生成玩家
    /// </summary>
    public void SpawnPlayerNearTargetDoor(int doorIndex)
    {
        RoomDoor targetDoor=GameObject.Find("Door_" + doorIndex.ToString()).GetComponent<RoomDoor>();
        GameManager.Instance.SpawnPlayerAtPoint(targetDoor.boundPoint);
    }


}
