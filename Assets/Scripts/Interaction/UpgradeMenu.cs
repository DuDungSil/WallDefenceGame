using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] 
    private GameObject upgradeMaterialSlotsParent;
    private UpgradeMaterialSlot[] upgradeMaterialSlots;

    public int numOfMaterialSlots = 6;

    public GameObject upgradeBtn;
    public GameObject maxText;

    private BuildingBOM bom;

    void Awake()
    {
        upgradeMaterialSlots = upgradeMaterialSlotsParent.GetComponentsInChildren<UpgradeMaterialSlot>();
    }


    public void SetUpgradeUI(StructureManager structureManager)
    {
        bom = structureManager.nextUpgrade;
        UIUpdate();
    }

    private void UIUpdate()
    {
        if(bom.Data == null)
        {
            maxText.SetActive(true);
            upgradeBtn.SetActive(false);
            UpgradeMaterialSlotsClear();
        }
        else
        {
            if(isCreateble())
            {
                upgradeBtn.SetActive(true);
            }
            else
            {
                upgradeBtn.SetActive(false);
            }
            maxText.SetActive(false);
            // 재료 슬롯 업데이트
            UpgradeMaterialSlotsUpdate();

        }
    }

    void UpgradeMaterialSlotsUpdate()
    {
        int numActiveSlot = numOfMaterialSlots;
        int numOfItems = bom.craftNeedItems.Length;

        numActiveSlot = numOfItems % numOfMaterialSlots == 0 ? numOfMaterialSlots : numOfItems % numOfMaterialSlots;
        
        for(int i = 0; i < numActiveSlot; i++)
        {
            upgradeMaterialSlots[i].gameObject.SetActive(true);
            upgradeMaterialSlots[i].setItem(bom.craftNeedItems[i], bom.Data.craftNeedItemCount[i], ResourceDatabase.Instance.getElementCount(bom.craftNeedItems[i]));        
        }

        for(int i = numActiveSlot; i < numOfMaterialSlots; i++)
        {
            upgradeMaterialSlots[i].gameObject.SetActive(false);
        }       
    }

    void UpgradeMaterialSlotsClear()
    {
        for(int i = 0; i < numOfMaterialSlots; i++)
        {
            upgradeMaterialSlots[i].gameObject.SetActive(false);       
        }
    }

    // 만들 수 있는지?
    private bool isCreateble()
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
}
