using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioClip[] BGMs;
    public AudioClip[] AmbientMusics;
    public AudioClip currBGM;
    public AudioClip currAmbientLoop;

    public AudioSource audioSource_BGM;
    public AudioSource audioSource_AmbientSound;
    public AudioSource audioSource_SpecialLoop;

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

    public void PlayAmbient(int index, bool isQuick)
    {
        if (currBGM != null)
        {
            if (isQuick)
            {
                TransitionMusic(audioSource_AmbientSound, BGMs[index], transitionDuration_Quick);
            }
            else
                TransitionMusic(audioSource_AmbientSound, BGMs[index], transitionDuration_Slow);
        }
        else
        {
            audioSource_AmbientSound.clip = BGMs[index];
            audioSource_AmbientSound.Play();
        }
    }
    public void PlayBGM(int index,bool isQuick)
    {
        if(currBGM!=null)
        {
            if (isQuick)
            {
                TransitionMusic(audioSource_BGM,BGMs[index], transitionDuration_Quick);
            }else
                TransitionMusic(audioSource_BGM,BGMs[index], transitionDuration_Slow);
        }else
        {
            audioSource_BGM.clip = BGMs[index];
            audioSource_BGM.Play();
        }
    }

    Sequence sequence;
    public void TransitionMusic(AudioSource source,AudioClip newMusicClip,float duration)
    {

        // 创建一个 DOTween 序列

        sequence.Kill(); 
        sequence = DOTween.Sequence();

        // 第一步：将音量降低到最低值
        sequence.Append(source.DOFade(minVolume, duration / 2));

        // 第二步：在音量降低完成后切换音乐
        sequence.AppendCallback(() =>
        {
            source.clip = newMusicClip;
            source.Play();
        });

        // 第三步：将新音乐的音量升高到正常值
        sequence.Append(source.DOFade(defaultVolume, duration / 2));

        // 启动序列
        sequence.Play();
    }


    public void PlaySoundEffect(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, 20f);

    }


    public void PlaySpecialSoundLoop(AudioClip clip)
    {
        audioSource_SpecialLoop.clip = clip;
        audioSource_SpecialLoop.Play();
    }

    public void EndSpecialSoundLoop()
    {
        audioSource_SpecialLoop.Stop();
        audioSource_SpecialLoop.clip = null;
    }

    private void Update()
    {
        transform.position = Camera.main.transform.position;
    }



}
