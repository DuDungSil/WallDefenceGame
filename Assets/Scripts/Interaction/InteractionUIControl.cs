using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionUIControl : MonoBehaviour
{
    public GameObject InteractionMenu;
    public GameObject defaultImage;
    public GameObject AssignUnitMenu;
    public GameObject DestroyMenu;
    public GameObject UpgradeMenu;

    private GameObject selectedObject;

    void Start()
    {
        ResourceDatabase.Instance.onItemChanged += UpdateUI;
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        InteractionMenu.SetActive(true);
        defaultImage.SetActive(true);
        AssignUnitMenu.SetActive(false);
        DestroyMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        //BuildingInteraction();
    }

    public void setInteractionObject(GameObject _obj)
    {
        selectedObject = _obj;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(selectedObject == null)
        {
            return;
        }
        else if(selectedObject.layer == LayerMask.NameToLayer("Structure"))
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
        DestroyMenu.SetActive(true);
        UpgradeMenu.SetActive(true);

        if(structureManager is AssignUnitStructureManager)
        {
            if(((AssignUnitStructureManager)structureManager).IsAssigned) // structure에 이미 유닛이 할당되어 있을 때
            {
                AssignUnitMenu.SetActive(true);
                AssignUnitMenu.GetComponent<AssignUnitMenu>().SetAlreadyAssignUI(((AssignUnitStructureManager)structureManager));
            }
            else
            {
                AssignUnitMenu.SetActive(true);
                AssignUnitMenu.GetComponent<AssignUnitMenu>().SetNeedAssignUI(((AssignUnitStructureManager)structureManager));
            }
        }

        // 디스트로이 메뉴
        DestroyMenu.GetComponent<DestroyMenu>().SetDestroyUI(structureManager);

        // 업그레이드 메뉴
        UpgradeMenu.GetComponent<UpgradeMenu>().SetUpgradeUI(structureManager);
        
    }

    public void ResourceInteraction()
    {
        ResourceManager resourceManager = selectedObject.GetComponent<ResourceManager>();
        if(resourceManager.IsAssigned) // structure에 이미 유닛이 할당되어 있을 때
        {
            AssignUnitMenu.GetComponent<AssignUnitMenu>().SetAlreadyAssignUI(resourceManager);
        }
        else
        {
            AssignUnitMenu.GetComponent<AssignUnitMenu>().SetNeedAssignUI(resourceManager);
        }
        defaultImage.SetActive(false);
        AssignUnitMenu.SetActive(true);
        DestroyMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
    }

    public void clickDestroy()
    {
        StructureManager structureManager = selectedObject.GetComponent<StructureManager>();
        structureManager.Selfdestroy();
        OnEnable();

        SoundController.Instance.PlaySound2D("Building_destroy");
    }

    public void clickUpgrade()
    {
        StructureManager structureManager = selectedObject.GetComponent<StructureManager>();
        selectedObject = structureManager.SelfUpgrade();

        StructureManager newStructureManager = selectedObject.GetComponent<StructureManager>();
        newStructureManager.TakeDamage(structureManager.MaxHp - structureManager.Hp);

        structureManager.Selfdestroy();
        UpdateUI();

        SoundController.Instance.PlaySound2D("Building_upgrade");
    }


}
