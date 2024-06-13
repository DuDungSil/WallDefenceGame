using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneSettingUIControl : MonoBehaviour
{
    public Slider MasterVol;
    public Slider BGMVol;
    public Slider SFXVol;

    void OnEnable() 
    {
        MasterVol.value = DecibelToLinear(OptionManager.Instance.masterVolume);
        BGMVol.value = DecibelToLinear(OptionManager.Instance.BGMVolume);
        SFXVol.value = DecibelToLinear(OptionManager.Instance.SFXVolume);
    }

    public void MasterVolChanged()
    {
        OptionManager.Instance.SetVolume(SoundType.Master, Mathf.Log10(MasterVol.value)*20);
    }
    public void BGMVolChanged()
    {
        OptionManager.Instance.SetVolume(SoundType.BGM, Mathf.Log10(BGMVol.value)*20);
    }
    public void SFXVolChanged()
    {
        OptionManager.Instance.SetVolume(SoundType.SFX, Mathf.Log10(SFXVol.value)*20);
    }

    float DecibelToLinear(float dBValue)
    {
        return Mathf.Pow(10f, dBValue / 20f);
    }
}
