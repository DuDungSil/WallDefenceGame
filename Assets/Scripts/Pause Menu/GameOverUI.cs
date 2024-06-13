using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
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
