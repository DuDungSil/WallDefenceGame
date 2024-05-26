using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public Button upgradeBtn;

    public void SetUpgradeUI(StructureManager structureManager)
    {
        Debug.Log("업그레이드 UI 켬");
        upgradeBtn.onClick.RemoveAllListeners();
        upgradeBtn.onClick.AddListener(structureManager.SelfUpgrade);
    }
}
