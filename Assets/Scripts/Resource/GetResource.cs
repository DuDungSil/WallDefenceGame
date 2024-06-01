using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetResource : MonoBehaviour
{
    public ResourceItemData resourceData;
    private float timer;
    [SerializeField]
    private float resourceGetTime;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > resourceGetTime)
        {
            //wood 개수, stone 개수 증가
            ResourceDatabase.Instance.AddItem((ResourceItem)resourceData.CreateItem());
            timer = 0f;
        }
    }
}
