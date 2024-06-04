using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class SettingSlot : MonoBehaviour
{
    GameObject displayItem;
    public float widthSize = 1;
    public float heightSize = 1;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // 슬롯 업데이트 함수          
    public void SlotUpadate(GameObject prefab)
    {
        if(displayItem) Destroy(displayItem);
        displayItem = Instantiate(prefab, gameObject.transform);

        Vector3 parentScale = transform.localScale;
        displayItem.transform.localScale = new Vector3(widthSize, heightSize * parentScale.x/parentScale.y, 1);
    }

    
}
