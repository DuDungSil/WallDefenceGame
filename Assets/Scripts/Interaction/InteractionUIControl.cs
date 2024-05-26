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

    public void BuildingInteraction(GameObject interactableObject)
    {
        selectedObject = interactableObject;
        StructureManager selectedObjectStructureManager = selectedObject.GetComponent<StructureManager>();
        defaultImage.SetActive(false);
        AssignUnitMenu.SetActive(true);
        DestroyMenu.SetActive(true);
        UpgradeMenu.SetActive(true);
        if(selectedObjectStructureManager.IsAssigned) // structure에 이미 유닛이 할당되어 있을 때
        {
            AssignUnitMenu.GetComponent<AssignUnitMenu>().SetAlreadyAssignUI(selectedObjectStructureManager);
        }
        else
        {
            AssignUnitMenu.GetComponent<AssignUnitMenu>().SetNeedAssignUI(selectedObjectStructureManager);
        }
        UpgradeMenu.GetComponent<UpgradeMenu>().SetUpgradeUI(selectedObjectStructureManager);
        DestroyMenu.GetComponent<DestroyMenu>().SetDestroyUI(selectedObjectStructureManager);
    }
    public void ResourceInteraction()
    {
        defaultImage.SetActive(false);
        AssignUnitMenu.SetActive(true);
        DestroyMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
    }
}
