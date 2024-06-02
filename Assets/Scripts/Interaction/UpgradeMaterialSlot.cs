using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMaterialSlot : MonoBehaviour
{
    private ResourceItem item;
    public Image meterialImage;
    public TMP_Text available_Count;
    public TMP_Text need_Count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setItem(ResourceItem _item, int _needCount, int _availableCount)
    {
        item = _item;
        meterialImage.sprite = item.Data.IconSprite;
        available_Count.text = _availableCount.ToString();
        need_Count.text = _needCount.ToString();
    }
}
