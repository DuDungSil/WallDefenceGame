using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionUIControl : MonoBehaviour
{
    public GameObject defaultImage;
    public GameObject AssignUnitMenu;
    public GameObject DestroyMenu;
    public GameObject UpgradeMenu;

    private GameObject selectedObject;


    // Start is called before the first frame update
    private void OnEnable()
    {
        defaultImage.SetActive(true);
        AssignUnitMenu.SetActive(false);
        DestroyMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        //BuildingInteraction();
    }

    public void setInteractionObject(GameObject _obj)
    {
        selectedObject = _obj;
        UIUpdate();
    }

    private void UIUpdate()
    {
        if(selectedObject.layer == LayerMask.NameToLayer("Structure"))
        {
            BuildingInteraction();
        }
        else if(selectedObject.layer == LayerMask.NameToLayer("Resource"))
        {
            ResourceInteraction();
        }
    }

    public void BuildingInteraction()
    {
        StructureManager structureManager = selectedObject.GetComponent<StructureManager>();

        defaultImage.SetActive(false);
        AssignUnitMenu.SetActive(true);
        DestroyMenu.SetActive(true);
        UpgradeMenu.SetActive(true);

        // 유닛 할당 버튼
        if(structureManager.IsAssigned) // structure에 이미 유닛이 할당되어 있을 때
        {
            AssignUnitMenu.GetComponent<AssignUnitMenu>().SetAlreadyAssignUI(structureManager);
        }
        else
        {
            AssignUnitMenu.GetComponent<AssignUnitMenu>().SetNeedAssignUI(structureManager);
        }

        // 디스트로이 버튼
        DestroyMenu.GetComponent<DestroyMenu>().SetDestroyUI(structureManager);

        // 업그레이드 버튼
        UpgradeMenu.GetComponent<UpgradeMenu>().SetUpgradeUI(structureManager);
        
    }

    public void ResourceInteraction()
    {
        defaultImage.SetActive(false);
        AssignUnitMenu.SetActive(true);
        DestroyMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
    }

    public void clickDestroy()
    {
        StructureManager structureManager = selectedObject.GetComponent<StructureManager>();
        structureManager.Selfdestroy();
        // ui 종료
    }

    public void clickUpgrade()
    {
        StructureManager structureManager = selectedObject.GetComponent<StructureManager>();
        structureManager.SelfUpgrade();
        UIUpdate();
    }


}
