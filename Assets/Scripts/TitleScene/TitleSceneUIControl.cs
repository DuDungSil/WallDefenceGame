using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneUIControl : MonoBehaviour
{
    public void GameExit()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }
}
