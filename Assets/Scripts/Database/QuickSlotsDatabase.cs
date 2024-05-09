using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotsDatabase : Singleton<QuickSlotsDatabase>
{
    List<EquipmentItem> quickslots = new List<EquipmentItem>(new EquipmentItem[4]);

    void Start()
    {
        // 퀵슬롯 초기화
        for(int i = 0; i < 4; i++) quickslots[i] = null;
    }

    void Update()
    {
        
    }

    public List<EquipmentItem> getQuickslots()
    {
        return quickslots;
    }

    public EquipmentItem getQuickslotsItem(int index)
    {
        return quickslots[index];
    }

    public void setQuickslots(int index, EquipmentItem item)
    {
        quickslots[index] = item;
    }

    public void deleteQuickslots(int index)
    {
        quickslots[index] = null;
    }    

    //퀵슬롯 장비 리스트
    // 특정 인덱스에 장비 추가
    // 특정 인덱스 null 처리 ( 삭제 )
}
