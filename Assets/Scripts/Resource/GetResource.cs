using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetResource : MonoBehaviour
{
    private float timer;
    [SerializeField]
    private float resourceGetTime;
    [SerializeField]
    private string resourceName;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > resourceGetTime)
        {
            //wood 개수, stone 개수 증가
            GameController.Instance.GetResource(resourceName);
            timer = 0f;
        }
    }
}
