using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPageManager : MonoBehaviour
{/*
    [SerializeField] 
    private GameObject CraftSlotsParent;
    [SerializeField] 
    private GameObject MaterialSlotsParent;

    private CraftSlot[] craftSlots;
    private MaterialSlot[] materialSlots;

    public int numOfCraftSlots = 2;  // 페이지당 크래프트 슬롯 수
    public int numOfMaterialSlots = 6;  // 페이지당 크래프트 슬롯 수

    private int category;
    private int currentPage;
    private int lastPage;

    // Start is called before the first frame update
    void Start()
    {
        // resourceDatabase = ResourceDatabase.Instance.getDatabase();
        craftSlots = CraftSlotsParent.GetComponentsInChildren<CraftSlot>();
        materialSlots = CraftSlotsParent.GetComponentsInChildren<MaterialSlot>();
        currentPage = 0;
        // lastPage = (resourceDatabase.Count-1) / numOfSlots;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void slotsUpdate()
    {
        CraftSlotsUpdate();
        MaterialSlotsUpdate();
    }

    void CraftSlotsUpdate()
    {
        int numActiveSlot = numOfCraftSlots;
        int startIndex = currentPage * numOfCraftSlots;

        if(currentPage == lastPage)
        {
            numActiveSlot = resourceDatabase.Count % numOfCraftSlots == 0 ? numOfCraftSlots : resourceDatabase.Count % numOfCraftSlots;
        }

        for(int i = 0; i < numActiveSlot; i++)
        {
            resourceSlots[i].gameObject.SetActive(true);
            resourceSlots[i].setItem(resourceDatabase[startIndex + i], resourceDatabase[startIndex + i].Amount);
        }

        for(int i = numActiveSlot; i < numOfCraftSlots; i++)
        {
            resourceSlots[i].gameObject.SetActive(false);
        }
    }

    void MaterialSlotsUpdate()
    {
        int numActiveSlot = numOfMaterialSlots;
        int startIndex = currentPage * numOfMaterialSlots;

        if(currentPage == lastPage)
        {
            numActiveSlot = resourceDatabase.Count % numOfCraftSlots == 0 ? numOfCraftSlots : resourceDatabase.Count % numOfCraftSlots;
        }

        for(int i = 0; i < numActiveSlot; i++)
        {
            resourceSlots[i].gameObject.SetActive(true);
            resourceSlots[i].setItem(resourceDatabase[startIndex + i], resourceDatabase[startIndex + i].Amount);
        }

        for(int i = numActiveSlot; i < numOfCraftSlots; i++)
        {
            resourceSlots[i].gameObject.SetActive(false);
        }       
    }

    public void previousPage()
    {
        if(currentPage > 0)
        {
            currentPage--;
            slotsUpdate();
        } 
        // 0번째로 가면 이전버튼 비활성화
    }

    public void nextPage()
    {
        if(currentPage < lastPage)
        {
            currentPage++;
            slotsUpdate();
        } 
        // 마지막 페이지면 다음버튼 비활성화
    }


    // 제작 가능한지 아닌지 판단해서 버튼 활성화/비활성화
    // 데이터 베이스부터 구축 필요
    */
}
