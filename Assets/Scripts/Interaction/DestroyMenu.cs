using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyMenu : MonoBehaviour
{
    public Button destroyBtn;
    
    public void SetDestroyUI(StructureManager structureManager)
    {
        Debug.Log("부수기 UI 켬");
        destroyBtn.onClick.RemoveAllListeners();
        destroyBtn.onClick.AddListener(structureManager.Selfdestroy);
    }
}
