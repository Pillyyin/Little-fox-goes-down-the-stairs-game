using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingPanel : MonoBehaviour
{
    [Header("設定面板物件")]
    public GameObject settingPanel ;

    [Header("Audio Mixer 設定")]
    public AudioMixer mainMixer ;

    // 設定面板ㄉ開關
    public void ToggleSettingPanel()
    {
        //防呆
        if(settingPanel != null)
        {
            //用activeSelf 取得當前顯示狀態
            bool isCurrentlyActive = settingPanel.activeSelf ;
            settingPanel.SetActive(!isCurrentlyActive) ;
        }
    }

    public void CloseSettingPanel()
    {
        if(settingPanel != null)
        {
            settingPanel.SetActive(false) ;
        }
    }


    // 控制背景音樂大小(BGM Slider)
    public void SetBGMVolume(float value)
    {
        if(mainMixer != null)
        {
            //slider 0 ~ 1 轉換分貝(dB: -80 ~ 0 )
            float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
            mainMixer.SetFloat("BGMVol", dB); 
        }
    }

    // 控制音效大小(SFX Slider)
    public void SetSFXVolume(float value)
    {
        if(mainMixer != null)
        {
            //slider 0 ~ 1 轉換分貝(dB: -80 ~ 0 )
            float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
            mainMixer.SetFloat("SFXVol", dB); 
        }
    }

}
