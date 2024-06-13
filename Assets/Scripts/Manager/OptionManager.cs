using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : Singleton<OptionManager>
{
    private static OptionManager s_Instance = null;

    public float masterVolume = 0f;
    public float BGMVolume = 0f;
    public float SFXVolume = 0f;

    public void InitAudioMixer()
    {
        SoundController.Instance.InitVolumes(masterVolume, BGMVolume, SFXVolume);
    }

    public void SetVolume(SoundType type, float value)
    {
        if(type == SoundType.Master)
        {
            masterVolume = value;
            SoundController.Instance.SetVolume(SoundType.Master, masterVolume);
        }
        else if(type == SoundType.BGM)
        {
            BGMVolume = value;
            SoundController.Instance.SetVolume(SoundType.BGM, BGMVolume);
        }
        else if(type == SoundType.SFX)
        {
            SFXVolume = value;
            SoundController.Instance.SetVolume(SoundType.SFX, SFXVolume);
        }
    }

    void Awake()
    {
        if(s_Instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }

        s_Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
