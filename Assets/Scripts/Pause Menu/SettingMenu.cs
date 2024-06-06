using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{

    public Slider MasterVol;
    public Slider BGMVol;
    public Slider SFXVol;

    void OnEnable() 
    {

    }

    public void MasterVolChanged()
    {
        SoundController.Instance.SetVolume(SoundType.Master, Mathf.Log10(MasterVol.value)*20);
    }
    public void BGMVolChanged()
    {
        SoundController.Instance.SetVolume(SoundType.BGM, Mathf.Log10(BGMVol.value)*20);
    }
    public void SFXVolChanged()
    {
        SoundController.Instance.SetVolume(SoundType.SFX, Mathf.Log10(SFXVol.value)*20);
    }
}
