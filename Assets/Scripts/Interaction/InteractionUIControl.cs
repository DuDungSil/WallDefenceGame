using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void BuildingInteraction()
    {
        defaultImage.SetActive(false);
        AssignUnitMenu.SetActive(true);
        DestroyMenu.SetActive(true);
        UpgradeMenu.SetActive(true);
    }
    public void ResourceInteraction()
    {
        defaultImage.SetActive(false);
        AssignUnitMenu.SetActive(true);
        DestroyMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
    }
}
