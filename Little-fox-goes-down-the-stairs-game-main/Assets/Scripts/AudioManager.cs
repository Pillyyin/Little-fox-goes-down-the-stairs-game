using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // 全域單例 (Singleton)，讓 Player 或其他物件可以直接呼叫
    public static AudioManager Instance;

    [Header("Audio Mixer")]
    public AudioMixerGroup bgmGroup ; // Mixer裡的BGM群組
    public AudioMixerGroup sfxGroup ; // Mixer裡的SFX群組

    [Header("Audio Source")]
    private AudioSource bgmSource ;
    private AudioSource sfxSource ;

    private void Awake()
    {
        // 確保整個遊戲中只有一個 AudioManager (跨場景不重複建立)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 切換場景時不銷毀這個物件
            InitAudioSources() ;
        }
        else
        {
            Destroy(gameObject) ; // 如果已經存在，就銷毀重複的
        }
    }

    // 初始化背景音樂與音效的播放器
    private void InitAudioSources()
    {
        // 建立背景音樂播放器
        bgmSource = gameObject.AddComponent<AudioSource>() ;
        bgmSource.loop = true; // BGM 自動循環
        if (bgmGroup != null) bgmSource.outputAudioMixerGroup = bgmGroup;

        // 建立音效播放器
        sfxSource = gameObject.AddComponent<AudioSource>() ;
        sfxSource.loop = false;
        if (sfxGroup != null) sfxSource.outputAudioMixerGroup = sfxGroup;
    }

    // 播放、切換背景音樂
    public void PlayBGM(AudioClip bgmClip)
    {
        if (bgmClip == null || bgmSource == null) return ;

        // 如果目前正在播放同一首音樂，就不重複重頭播放
        if (bgmSource.clip == bgmClip && bgmSource.isPlaying) return;

        bgmSource.clip = bgmClip;
        bgmSource.loop = true; // 設置背景音樂循環播放
        bgmSource.Play();
    }

    // 播放單次音效 (平台、受傷、跳躍)
    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        if (clip == null) return ;

        // PlayOneShot 可以讓多個音效重疊播放也不會互相蓋掉
        sfxSource.PlayOneShot(clip, volume) ;
    }

    public void PauseBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Pause() ;
        }
    }

    // 停止背景音樂
    public void StopBGM()
    {
        bgmSource.Stop() ;
    }

    

}
