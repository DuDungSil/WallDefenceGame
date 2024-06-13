using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuControl : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingPanel;

    void OnEnable() 
    {
        Time.timeScale = 0;
        mainPanel.SetActive(true);
        settingPanel.SetActive(false);
    }
    void OnDisable() 
    {
        Time.timeScale = 1;
    }

    public void ClcikResumeBtn()
    {
        gameObject.SetActive(false);
    }

    public void ClcikSettingBtn()
    {
        mainPanel.SetActive(false);
        settingPanel.SetActive(true);
    }

    public void ClcikExitBtn()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ClcikBackBtn()
    {
        mainPanel.SetActive(true);
        settingPanel.SetActive(false);
    }

}
