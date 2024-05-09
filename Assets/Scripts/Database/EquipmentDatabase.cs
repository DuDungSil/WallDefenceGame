using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentDatabase : Singleton<EquipmentDatabase>
{
    List<EquipmentItem> createdEquipments = new List<EquipmentItem>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public List<EquipmentItem> getDatabase()
    {
        return createdEquipments;
    }

    // 아이템 추가 함수
    //  처음 추가한 아이템이라면 아이템의 id를 보고 리스트를 재정렬
    public void AddItem(EquipmentItem _item)
    {
        int index = FindIndexByCondition(createdEquipments, equipment => equipment.Data.ID == _item.Data.ID);
        if(index == -1)
        {
            createdEquipments.Add(_item);
            createdEquipments = createdEquipments.OrderBy(x => x.Data.ID).ToList(); // id 순으로 재정렬
        }
        OnItemChanged();
    }

    // 아이템 삭제 함수
    public void DeleteItem(Item _item)
    {
        int index = FindIndexByCondition(createdEquipments, equipment => equipment.Data.ID == _item.Data.ID);
        if(index == -1)
        {
            Debug.Log("없는 아이템을 제거하려고 시도하였음");
        }
        else
        {
            createdEquipments.RemoveAt(index);
        }
        OnItemChanged();
    }

    int FindIndexByCondition(List<EquipmentItem> list, Predicate<EquipmentItem> condition)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (condition(list[i]))
            {
                return i;
            }
        }
        return -1; // 조건을 만족하는 객체가 없을 경우
    }

    // 옵서버 패턴
    // 아이템 변경 이벤트 델리게이트
    public delegate void ItemChanged();
    public event ItemChanged onItemChanged;

    // 아이템 변경 이벤트 호출 메서드
    private void OnItemChanged()
    {
        if (onItemChanged != null)
            onItemChanged.Invoke();
    }

    // 싱글톤
    
    // 만든 장비 리스트
    //  둘다 추가 삭제 구현
    //  id로 정렬
}
