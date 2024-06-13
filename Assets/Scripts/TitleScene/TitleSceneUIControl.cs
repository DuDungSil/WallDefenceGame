using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneUIControl : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingPanel;

    public void Start()
    {
        mainPanel.SetActive(true);
        settingPanel.SetActive(false);
        OptionManager.Instance.InitAudioMixer();
    }  

    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }

    public void ClickSettingBtn()
    {
        mainPanel.SetActive(false);
        settingPanel.SetActive(true);
    }

    public void ClickBackBtn()
    {
        mainPanel.SetActive(true);
        settingPanel.SetActive(false);
    }  

    public void GameExit()
    {
        Application.Quit();
    }

}
