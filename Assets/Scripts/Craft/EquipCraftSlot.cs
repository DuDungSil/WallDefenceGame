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

    private EquipmentBOM bom;


    public int numOfMaterialSlots = 6;  // 페이지당 머테리얼 슬롯 수

    public Image Image;
    public TMP_Text text_Name;
    public GameObject button;
    public GameObject craftedText;

    // Start is called before the first frame update
    void Awake()
    {
        materialSlots = MaterialSlotsParent.GetComponentsInChildren<MaterialSlot>();
    }

    void MaterialSlotsUpdate()
    {
        int numActiveSlot = numOfMaterialSlots;
        int numOfItems = bom.craftNeedItems.Length;

        numActiveSlot = numOfItems % numOfMaterialSlots == 0 ? numOfMaterialSlots : numOfItems % numOfMaterialSlots;
        
        for(int i = 0; i < numActiveSlot; i++)
        {
            materialSlots[i].gameObject.SetActive(true);
            materialSlots[i].setItem(bom.craftNeedItems[i], bom.Data.craftNeedItemCount[i]);
            
            if(bom.isCrafted == true)
            {
                craftedText.SetActive(true);
                button.SetActive(false);
            }
            else
            {
                craftedText.SetActive(false);
                button.SetActive(isCreateble());
            }         
        }

        for(int i = numActiveSlot; i < numOfMaterialSlots; i++)
        {
            materialSlots[i].gameObject.SetActive(false);
        }       
    }

    // 만들 수 있는지?
    public bool isCreateble()
    {
        for(int i = 0; i < bom.craftNeedItems.Length; i++)
        {
            int count = ResourceDatabase.Instance.getElementCount(bom.craftNeedItems[i]);
            if(count == -1 || bom.Data.craftNeedItemCount[i] > count)
            {
                return false;
            }
        }
        return true;
    }


    // 한번 만들었던 건 업그레이드로 버튼 교체

    public void craftButtonClick()
    {

        bom.isCrafted = true;
        
        // 장비DB 아이템 추가 ( 아이템 클래스로 비교 ), bom 을 맨 뒤로
        if(bom.Data.itemdata is MeleeWeaponItemData)
        {
            EquipmentDatabase.Instance.AddItem((EquipmentItem)bom.Data.itemdata.CreateItem(), 0);
            EquipmentCraftManual.Instance.MoveElementToEnd(bom, 0);
        }
        else if(bom.Data.itemdata is RangedWeaponItemData)
        {
            EquipmentDatabase.Instance.AddItem((EquipmentItem)bom.Data.itemdata.CreateItem(), 1);
            EquipmentCraftManual.Instance.MoveElementToEnd(bom, 1);
        }
        // 필요 아이템 소모
        ResourceDatabase.Instance.DecreaseResource(bom.craftNeedItems, bom.Data.craftNeedItemCount);        
    }

    void Update()
    {
        
    }

    

    public void setItem(EquipmentBOM _bom)
    {
        Image.sprite = _bom.item.Data.IconSprite;
        text_Name.text = _bom.item.Data.Name;
        bom = _bom;
        MaterialSlotsUpdate();
    }
}
