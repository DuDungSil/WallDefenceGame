using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class EquipmentBOM 
{
    public EquipmentBOMData Data;

    [HideInInspector] 
    public EquipmentItem item;
    [HideInInspector] 
    public ResourceItem[] craftNeedItems;  // 필요한 아이템

    [HideInInspector] 
    public bool isCrafted = false;


    public void Init()
    {
        item = (EquipmentItem)Data.itemdata.CreateItem();
        craftNeedItems = new ResourceItem[Data.craftNeedItemDatas.Length];
        for(int i = 0; i < Data.craftNeedItemDatas.Length; i++)
        {
            craftNeedItems[i] = (ResourceItem)Data.craftNeedItemDatas[i].CreateItem();
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

    public void MoveElementToEnd(EquipmentBOM element, int manualIndex)
    {
        EquipmentBOM[] Manual = null;
        if(manualIndex == 0) Manual = meleeweapon_Manual;
        else if(manualIndex == 1) Manual = rangedweapon_Manual;

        if (Manual == null || Manual.Length == 0)
            return; // 배열이 비어있거나 null인 경우 처리하지 않음

        int index = Array.IndexOf(Manual, element);

        EquipmentBOM firstElement = Manual[index]; // 요소 저장
        
        for (int i = index; i < Manual.Length - 1; i++)
        {
            Manual[i] = Manual[i + 1]; // 모든 요소를 한 칸 앞으로 이동
        }

        Manual[Manual.Length - 1] = firstElement; // 마지막 자리에 첫 번째 요소 배치
    }

    void Start()
    {
        foreach(var bom in  meleeweapon_Manual) bom.Init();
        foreach(var bom in  rangedweapon_Manual) bom.Init();
    }

    void Update()
    {
        
    }
}
