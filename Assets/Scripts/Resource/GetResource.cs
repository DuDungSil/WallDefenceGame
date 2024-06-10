using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetResource : MonoBehaviour
{
    public ResourceItemData resourceData;
    public int getResourceNum;
    private float timer;
    [SerializeField]
    private float resourceGetTime;


    void Update()
    {
        timer += Time.deltaTime;
        if(timer > resourceGetTime)
        {
            //wood 개수, stone 개수 증가
            ResourceDatabase.Instance.AddItem((ResourceItem)resourceData.CreateItem(), getResourceNum);
            timer = 0f;
        }
    }
}
