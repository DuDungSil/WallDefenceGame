using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialSlot : MonoBehaviour
{
    private ResourceItem item;
    private int itemCount;
    public Image itemImage;
    public TMP_Text text_Count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setItem(ResourceItem _item, int _itemCount)
    {
        item = _item;
        itemCount = _itemCount;
        itemImage.sprite = item.Data.IconSprite;
        text_Count.text = itemCount.ToString();
    }

}
