using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceDatabase : Singleton<ResourceDatabase>
{
    List<ResourceItem> resources = new List<ResourceItem>();

    void Start()
    {
        
    }

    public List<ResourceItem> getDatabase()
    {
        return resources;
    }

    // 아이템 추가 함수
    //  처음 추가한 아이템이라면 아이템의 id를 보고 리스트를 재정렬
    //  이미 있다면 갯수 증가
    public void AddItem(ResourceItem _item, int _count = 1)
    {
        int index = FindIndexByCondition(resources, resource => resource.Data.ID == _item.Data.ID);
        if(index == -1)
        {
            resources.Add(_item);
            resources = resources.OrderBy(x => x.Data.ID).ToList(); // id 순으로 재정렬
        }
        else
        {
            resources[index].SetAmount(resources[index].Amount + _count);
        }
        OnItemChanged();
    }

    // 아이템 감소 함수
    //  한번이라도 추가한 아이템이라면 0개가 되어도 사라지지 않음
    public void DeleteItem(Item _item, int _count = 1)
    {
        int index = FindIndexByCondition(resources, resource => resource.Data.ID == _item.Data.ID);
        if(index == -1)
        {
            Debug.Log("없는 아이템을 제거하려고 시도하였음");
        }
        else
        {
            resources[index].SetAmount(resources[index].Amount -_count);
        }
        OnItemChanged();
    }

    int FindIndexByCondition(List<ResourceItem> list, Predicate<ResourceItem> condition)
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

}
