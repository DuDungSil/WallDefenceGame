using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentBOM
{
    public string craftName;
    [HideInInspector] 
    public EquipmentItem item;
    public EquipmentItemData itemdata;
    [HideInInspector] 
    public ResourceItem[] craftNeedItems;  // 필요한 아이템
    public ResourceItemData[] craftNeedItemDatas; 
    public int[] craftNeedItemCount; // 필요한 아이템의 개수

    public void Init()
    {
        item = (EquipmentItem)itemdata.CreateItem();
        craftNeedItems = new ResourceItem[craftNeedItemDatas.Length];
        for(int i = 0; i < craftNeedItemDatas.Length; i++)
        {
            craftNeedItems[i] = (ResourceItem)craftNeedItemDatas[i].CreateItem();
        }
    }

}

public class EquipmentCraftManual : Singleton<EquipmentCraftManual>
{

    [SerializeField]
    private EquipmentBOM[] meleeweapon_Manual;  // 근접무기 메뉴얼 

    [SerializeField]
    private EquipmentBOM[] rangedweapon_Manual;  // 원거리무기 메뉴얼 

    public EquipmentBOM[] getMeleeweaponManual()
    {
        return meleeweapon_Manual;
    }

    public EquipmentBOM[] getRangedweaponManual()
    {
        return rangedweapon_Manual;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(var bom in  meleeweapon_Manual) bom.Init();
        foreach(var bom in  rangedweapon_Manual) bom.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
