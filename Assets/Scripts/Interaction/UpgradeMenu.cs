using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject upgradeBtn;
    public BuildingBOM bom;

    private void UIUpdate()
    {
        if(bom == null)
        {
            upgradeBtn.SetActive(false);
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

            // 재료 슬롯 업데이트

        }
    }

    public void SetUpgradeUI(StructureManager structureManager)
    {
        bom = structureManager.nextUpgrade;
        UIUpdate();
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
