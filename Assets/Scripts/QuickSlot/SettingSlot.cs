using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingSlot : MonoBehaviour
{
    GameObject displayItem;
    public float widthSize = 2;
    public float heightSize = 2;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // 슬롯 업데이트 함수          슬롯 아이템 위치 밑으로 계산 ?
    public void SlotUpadate(GameObject prefab)
    {
        if(displayItem) Destroy(displayItem);
        displayItem = Instantiate(prefab, gameObject.transform);

        // Vector3 Scale = displayItem.transform.localScale;
        // displayItem.transform.localScale = new Vector3(Scale.x * widthSize, Scale.y * heightSize, Scale.z);
    }

    
}
