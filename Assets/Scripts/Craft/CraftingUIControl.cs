using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUIControl : MonoBehaviour
{
    public GameObject Building_base;
    public GameObject Equipment_base;
    public void ActivateBilding()
    {
        Building_base.SetActive(true);
        Equipment_base.SetActive(false);
    }

    public void ActivateEquipment()
    {
        Building_base.SetActive(false);
        Equipment_base.SetActive(true);
    }

    void Start()
    {
        ActivateBilding();
    }

}
