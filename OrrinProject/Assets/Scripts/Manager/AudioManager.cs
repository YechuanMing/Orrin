using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioClip[] BGMs;

    public AudioClip currBGM;

    public AudioSource audioSource_BGM;
    public AudioSource audioSource_UI;

    public float defaultVolume = 0.5f;
    public float minVolume=0.1f;
    public float transitionDuration_Slow=4f;
    public float transitionDuration_Quick = 2f;

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
    }

    public void PlayBGM(int index,bool isQuick)
    {
        if(currBGM!=null)
        {
            if (isQuick)
            {
                TransitionMusic(BGMs[index], transitionDuration_Quick);
            }else
                TransitionMusic(BGMs[index], transitionDuration_Slow);
        }else
        {
            audioSource_BGM.clip = BGMs[index];
            audioSource_BGM.Play();
        }
    }

    Sequence sequence;
    public void TransitionMusic(AudioClip newMusicClip,float duration)
    {

        // 创建一个 DOTween 序列

        sequence.Kill(); 
        sequence = DOTween.Sequence();

        // 第一步：将音量降低到最低值
        sequence.Append(audioSource_BGM.DOFade(minVolume, duration / 2));

        // 第二步：在音量降低完成后切换音乐
        sequence.AppendCallback(() =>
        {
            audioSource_BGM.clip = newMusicClip;
            audioSource_BGM.Play();
        });

        // 第三步：将新音乐的音量升高到正常值
        sequence.Append(audioSource_BGM.DOFade(defaultVolume, duration / 2));

        // 启动序列
        sequence.Play();
    }


    public void PlaySoundEffect(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, 5f);
    }
}
