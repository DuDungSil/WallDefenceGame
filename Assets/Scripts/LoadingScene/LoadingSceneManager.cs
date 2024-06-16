using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public CircleFillHandler handler;
    public TMP_Text tmp;
    public static string nextScene;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        handler.fillValue = 0;
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                handler.fillValue = Mathf.Lerp(handler.fillValue, op.progress * 100f, timer);
                tmp.text = Mathf.FloorToInt(handler.fillValue).ToString();
                if (handler.fillValue >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                handler.fillValue = Mathf.Lerp(handler.fillValue, 100f, timer);
                tmp.text = Mathf.FloorToInt(handler.fillValue).ToString();
                if (handler.fillValue == 100f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
