using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPageManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject CraftSlotsParent;

    private EquipCraftSlot[] craftSlots;

    private EquipmentBOM[] meleeweapon_Manual;
    private EquipmentBOM[] rangedweapon_Manual;
    private EquipmentBOM[] current_manual;

    public int numOfCraftSlots = 2;  // 페이지당 크래프트 슬롯 수

    private int category;
    private int currentPage;
    private int lastPage;

    void Start()
    {
        meleeweapon_Manual = EquipmentCraftManual.Instance.getMeleeweaponManual();
        rangedweapon_Manual = EquipmentCraftManual.Instance.getRangedweaponManual();
        craftSlots = CraftSlotsParent.GetComponentsInChildren<EquipCraftSlot>();
        setCategory(0);
        CraftSlotsUpdate();

        ResourceDatabase.Instance.onItemChanged += UpdateUI;
    }

    void Update()
    {
        
    }

    void CraftSlotsUpdate()
    {
        int numActiveSlot = numOfCraftSlots;
        int startIndex = currentPage * numOfCraftSlots;

        if(currentPage == lastPage)
        {
            numActiveSlot = current_manual.Length % numOfCraftSlots == 0 ? numOfCraftSlots : current_manual.Length % numOfCraftSlots;
        }

        for(int i = 0; i < numActiveSlot; i++)
        {
            craftSlots[i].gameObject.SetActive(true);
            craftSlots[i].setItem(current_manual[startIndex + i]);
        }

        for(int i = numActiveSlot; i < numOfCraftSlots; i++)
        {
            craftSlots[i].gameObject.SetActive(false);
        }
    }

    public void previousPage()
    {
        if(currentPage > 0)
        {
            currentPage--;
            CraftSlotsUpdate();
        } 
        // 0번째로 가면 이전버튼 비활성화
    }

    public void nextPage()
    {
        if(currentPage < lastPage)
        {
            currentPage++;
            CraftSlotsUpdate();
        } 
        // 마지막 페이지면 다음버튼 비활성화
    }

    public void setCategory(int i)
    {
        category = i;
        currentPage = 0;

        if(category == 0)
        {
            current_manual = meleeweapon_Manual;
        }
        else if(category == 1)
        {
            current_manual = rangedweapon_Manual;
        }

        lastPage = (current_manual.Length-1) / numOfCraftSlots;
        CraftSlotsUpdate();
    }

    private void UpdateUI()
    {
        CraftSlotsUpdate();
    }

    // 제작 가능한지 아닌지 판단해서 버튼 활성화/비활성화
    // 데이터 베이스부터 구축 필요
    
}
