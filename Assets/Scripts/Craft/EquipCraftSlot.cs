using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipCraftSlot : MonoBehaviour
{
    [SerializeField] 
    private GameObject MaterialSlotsParent;
    private MaterialSlot[] materialSlots;

    private ResourceItem[] needitem;
    private int[] needitem_count;

    public int numOfMaterialSlots = 6;  // 페이지당 머테리얼 슬롯 수

    public Image Image;
    public TMP_Text text_Name;
    public GameObject button;

    // Start is called before the first frame update
    void Awake()
    {
        materialSlots = MaterialSlotsParent.GetComponentsInChildren<MaterialSlot>();
    }

    void MaterialSlotsUpdate()
    {
        int numActiveSlot = numOfMaterialSlots;
        int numOfItems = needitem.Length;

        numActiveSlot = numOfItems % numOfMaterialSlots == 0 ? numOfMaterialSlots : numOfItems % numOfMaterialSlots;
        
        for(int i = 0; i < numActiveSlot; i++)
        {
            materialSlots[i].gameObject.SetActive(true);
            materialSlots[i].setItem(needitem[i], needitem_count[i]);
            button.SetActive(isCreateble());
        }

        for(int i = numActiveSlot; i < numOfMaterialSlots; i++)
        {
            materialSlots[i].gameObject.SetActive(false);
        }       
    }

    // 만들 수 있는지?
    public bool isCreateble()
    {
        for(int i = 0; i < needitem.Length; i++)
        {
            int count = ResourceDatabase.Instance.getElementCount(needitem[i]);
            if(count == -1 || needitem_count[i] > count)
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void setItem(EquipmentBOM bom)
    {
        Image.sprite = bom.item.Data.IconSprite;
        text_Name.text = bom.item.Data.Name;
        needitem = bom.craftNeedItems;
        needitem_count = bom.craftNeedItemCount;
        MaterialSlotsUpdate();
    }
}
