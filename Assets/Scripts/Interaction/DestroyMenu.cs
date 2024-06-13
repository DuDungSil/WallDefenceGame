using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DestroyMenu : MonoBehaviour
{
    public TMP_Text structureName;
    public TMP_Text structureTier;
    private StructureManager structureManager;

    
    public void SetDestroyUI(StructureManager _structureManager)
    {
        structureManager = _structureManager;
        UIUpdate();
    }

    private void UIUpdate()
    {
        structureName.text = structureManager.structureName;
        structureTier.text = structureManager.grade.ToString();
    }
}
