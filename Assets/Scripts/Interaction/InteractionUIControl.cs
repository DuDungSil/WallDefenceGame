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
    // Start is called before the first frame update
    void Start()
    {
        defaultImage.SetActive(true);
        AssignUnitMenu.SetActive(false);
        DestroyMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
        //BuildingInteraction();
    }

    public void BuildingInteraction(BaseInteractionEventArgs args)
    {
        if (args is SelectEnterEventArgs selectArgs)
        {
            var selectedObject = selectArgs.interactableObject; //selectedObject.transform.gameObject 형식으로 게임오브젝트로서 사용하면 됨
            StructureManager structureManager = selectedObject.transform.gameObject.GetComponent<StructureManager>();
            defaultImage.SetActive(false);
            AssignUnitMenu.SetActive(true);
            DestroyMenu.SetActive(true);
            UpgradeMenu.SetActive(true);
            if(structureManager.isAssigned) // structure에 이미 유닛이 할당되어 있을 때
            {
                AssignUnitMenu.GetComponent<AssignUnitMenu>().alreadyAssign(structureManager);
            }
            else
            {
                AssignUnitMenu.GetComponent<AssignUnitMenu>().needAssign(structureManager);
            }
        }
    }
    public void ResourceInteraction()
    {
        defaultImage.SetActive(false);
        AssignUnitMenu.SetActive(true);
        DestroyMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
    }
}
