using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingCraftSlot : MonoBehaviour
{
    [SerializeField] 
    private GameObject MaterialSlotsParent;
    private MaterialSlot[] materialSlots;

    private GameObject craft_prefab;
    private GameObject craft_preview;

    private ResourceItem[] needitem;
    private int[] needitem_count;

    public int numOfMaterialSlots = 6;  // 페이지당 머테리얼 슬롯 수

    public Image Image;
    public TMP_Text text_Name;
    public GameObject button;

    void Awake()
    {
        materialSlots = MaterialSlotsParent.GetComponentsInChildren<MaterialSlot>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
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

    public void craftButtonClick()
    {
        Craft.Instance.SetCraft(craft_prefab, craft_preview, needitem, needitem_count);
        Craft.Instance.CraftButtonClick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setItem(BuildingBOM bom)
    {
        Image.sprite = bom.buildingImage;
        text_Name.text = bom.craftName;
        craft_prefab = bom.prefab;
        craft_preview = bom.PreviewPrefab;
        needitem = bom.craftNeedItems;
        needitem_count = bom.craftNeedItemCount;
        MaterialSlotsUpdate();
    }
}
