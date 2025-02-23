using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;
using System;

public class PostProcessManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static PostProcessManager Instance { get; private set; }

    private void Awake()
    {
        // 检查是否已有实例
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 设置实例并标记为不销毁
        Instance = this;
        DontDestroyOnLoad(gameObject);


    }

    private PostProcessVolume volume;


    private Vignette vignette;
    private ChromaticAberration chromatic;

    [Serializable]
    public struct PlayerDamageVignetteData
    {
        public Color damageColor;
        [Range(0.3f, 0.75f)]
        public float peakIntensity;
        [Range(0.1f, 0.5f)]
        public float upDuration;
        [Range(0.5f, 5f)]
        public float declineDuration;
    }

    [Serializable]
    public struct PlayerDashVignetteData
    {
        public Color damageColor;
        [Range(0.3f, 0.75f)]
        public float peakIntensity;
        [Range(0.1f, 0.5f)]
        public float upDuration;
        [Range(0.5f, 5f)]
        public float declineDuration;
    }

    [Serializable]
    public struct PlayerDieVignetteData
    {
        [Range(0.3f, 1f)]
        public float peakIntensity_Die;
        [Range(0.1f, 2f)]
        public float upDuration_Die;
    }

    [Serializable]
    public struct PlayerSpiritualizeComboData
    {
        [Header("Vignette")]
        public Color SprColor;
        [Range(0.3f, 0.75f)]
        public float peakIntensity;
        [Range(0.1f, 0.5f)]
        public float upDuration;

        [Header("Chrom")]
        [Range(0.1f, 1f)]
        public float chromIntensity;
    }

    [Range(0, 0.5f)]
    public float defaultIntensity;

    public PlayerDamageVignetteData playerDamageVignetteData;

    public PlayerDamageVignetteData playerDashVignetteData;

    public PlayerDieVignetteData playerDieVignetteData;

    public PlayerSpiritualizeComboData playerSpiritualizeComboData;


    private Tween vignetteTween;

    private void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<Vignette>(out vignette);
        volume.profile.TryGetSettings<ChromaticAberration>(out chromatic);
        ResetToDefault();
    }

    //重置
    public void ResetToDefault()
    {
        ResetVignette();
        ResetChromatic();
    }

    //重置
    public void ResetVignette()
    {
        vignette.intensity.value = defaultIntensity;
        vignette.color.value = Color.black;

    }

    //重置
    public void ResetChromatic()
    {
        chromatic.intensity.value = 0;
    }

    //玩家被攻击时后处理效果
    public void PlayerDamagedVignette()
    {
        vignette.color.value = playerDamageVignetteData.damageColor;

        vignetteTween.Kill();
        vignetteTween = DOTween.To(
           () => vignette.intensity.value,             // getter: 获取当前值
           x => vignette.intensity.value = x,          // setter: 设置新值
           playerDamageVignetteData.peakIntensity,                       // 目标值
           playerDamageVignetteData.upDuration * Time.timeScale                         // 持续时间
       ).OnComplete(() =>
       {
           vignetteTween = DOTween.To(
          () => vignette.intensity.value,             // getter: 获取当前值
           x => vignette.intensity.value = x,          // setter: 设置新值
           defaultIntensity,                       // 目标值
           playerDamageVignetteData.declineDuration            // 持续时间
       ).OnComplete(() => { vignette.color.value = new Color(0, 0, 0, 1); });
       }); // 实时打印值
    }

    public void PlayerDashVignette()
    {
        vignette.color.value = playerDashVignetteData.damageColor;

        vignetteTween.Kill();
        vignetteTween = DOTween.To(
           () => vignette.intensity.value,             // getter: 获取当前值
           x => vignette.intensity.value = x,          // setter: 设置新值
           playerDashVignetteData.peakIntensity,                       // 目标值
           playerDashVignetteData.upDuration * Time.timeScale                         // 持续时间
       ).OnComplete(() =>
       {
           vignetteTween = DOTween.To(
          () => vignette.intensity.value,             // getter: 获取当前值
           x => vignette.intensity.value = x,          // setter: 设置新值
           defaultIntensity,                       // 目标值
           playerDashVignetteData.declineDuration            // 持续时间
       ).OnComplete(() => { vignette.color.value = new Color(0, 0, 0, 1); });


       }); // 实时打印值
    }

    //玩家死亡时后处理效果
    public void PlayerDieVignette()
    {
        vignette.color.value = new Color(0, 0, 0, 1);
        vignetteTween.Kill();
        vignetteTween = DOTween.To(
           () => vignette.intensity.value,             // getter: 获取当前值
           x => vignette.intensity.value = x,          // setter: 设置新值
           playerDieVignetteData.peakIntensity_Die,                       // 目标值
          GameManager.Instance.rebornTime * 0.4f * Time.timeScale                         // 持续时间
       ).OnComplete(() =>
       {
           DOVirtual.DelayedCall(GameManager.Instance.rebornTime * 0.3f * Time.timeScale, () =>
           {
               DOTween.To(
               () => vignette.intensity.value,             // getter: 获取当前值
               x => vignette.intensity.value = x,          // setter: 设置新值
               defaultIntensity,                       // 目标值
               GameManager.Instance.rebornTime * 0.3f * Time.timeScale);
           });
           // 持续时间
       }); // 实时打印值
    }

    //灵魂化后处理效果综合
    public void PlayerSpiritualizeCombo()
    {
        //vignette
        vignette.color.value = playerSpiritualizeComboData.SprColor;
        vignetteTween.Kill();
        vignetteTween = DOTween.To(
           () => vignette.intensity.value,             // getter: 获取当前值
           x => vignette.intensity.value = x,          // setter: 设置新值
           playerSpiritualizeComboData.peakIntensity,                       // 目标值
           playerSpiritualizeComboData.upDuration * Time.timeScale                         // 持续时间
       );

        //chromatic
        chromatic.intensity.value = playerSpiritualizeComboData.chromIntensity;
    }
}
