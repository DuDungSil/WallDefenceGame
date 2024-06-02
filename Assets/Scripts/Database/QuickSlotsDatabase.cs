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

    public void saveQuickslotsItem(int index, EquipmentItem _item)
    {
        quickslots[index] = _item;
        if(_item is RangedWeaponItem)
        {
            EquipmentDatabase.Instance.SaveItem(_item , 1);
        }
        if(_item is MagicalWeaponItem)
        {
            EquipmentDatabase.Instance.SaveItem(_item , 2);
        }
    }

    public EquipmentItem getQuickslotsItem(int index)
    {
        return quickslots[index];
    }

    public void setQuickslotsItem(int index, EquipmentItem item)
    {
        quickslots[index] = item;
        OnItemChanged();
    }

    public void deleteQuickslotsItem(int index)
    {
        quickslots[index] = null;
    }    

    public delegate void ItemChanged();
    public event ItemChanged onItemChanged;

    // 아이템 변경 이벤트 호출 메서드
    private void OnItemChanged()
    {
        if (onItemChanged != null)
            onItemChanged.Invoke();
    }

    //퀵슬롯 장비 리스트
    // 특정 인덱스에 장비 추가
    // 특정 인덱스 null 처리 ( 삭제 )
}
