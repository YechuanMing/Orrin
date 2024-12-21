using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

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

    [Range(0,0.5f)]
    public float defaultIntensity;

    [Header("PlayerDamageVignette")]
    [Range(0.3f, 0.75f)]
    public float peakIntensity;
    [Range(0.1f, 0.5f)]
    public float upDuration;
    [Range(0.5f, 5f)]
    public float declineDuration;

    private Tween vignetteTween;

    private void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings<Vignette>(out vignette);
        vignette.intensity.value = defaultIntensity;
    }

    public void PlayerDamagedVignette()
    {
        //Color damageColor = new Color(80, 25, 25,255);
        //vignette.color.value = damageColor;
        vignetteTween.Kill();
        vignetteTween=DOTween.To(
           () => vignette.intensity.value,             // getter: 获取当前值
           x => vignette.intensity.value = x,          // setter: 设置新值
           peakIntensity,                       // 目标值
           upDuration*Time.timeScale                         // 持续时间
       ).OnComplete(() =>
       {
           vignetteTween = DOTween.To(
          () => vignette.intensity.value,             // getter: 获取当前值
           x => vignette.intensity.value = x,          // setter: 设置新值
           defaultIntensity,                       // 目标值
           declineDuration                         // 持续时间
       ).OnComplete(()=> { /*vignette.color.value = new Color(0, 0, 0,255);*/ });
       }); // 实时打印值
    }
}
