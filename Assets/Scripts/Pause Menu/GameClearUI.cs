using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearUI : MonoBehaviour
{
    void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void ClcikExitBtn()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
