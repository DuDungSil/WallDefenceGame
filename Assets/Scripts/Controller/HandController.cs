using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : Singleton<HandController>
{
    public GameObject LeftUIController;
    public GameObject RightUIController;
    public GameObject LeftGrabController;
    public GameObject RightGrabController;
    public XRInteractionManager interactionManager;
    private EquipmentItem equipItem;
    private GameObject equipObj;


    bool isUIController = false;
    bool isGrabController = true;

    public void SetUIController()
    {
        LeftGrabController.SetActive(false);
        RightGrabController.SetActive(false);
        LeftUIController.SetActive(true);
        RightUIController.SetActive(true);
        isUIController = true;
        isGrabController = false;
    }

    public void SetGrabController()
    {
        LeftUIController.SetActive(false);
        RightUIController.SetActive(false);
        LeftGrabController.SetActive(true);
        RightGrabController.SetActive(true);
        isUIController = false;
        isGrabController = true;
    }

    // 장비 장착
    public void SetRightHandEquipment(int index)
    {
        DeleteEquipObject();
        setEquipment(index);
    }

    // 소환했던 장비 오브젝트 삭 제
    public void DeleteEquipObject()
    {
        if(equipObj != null)
        {
            Destroy(equipObj);
        }
    }


    public void setEquipment(int index)
    {
        // 원래 장비 삭제
        if(equipObj != null)
        {
            Destroy(equipObj);
        }

        //퀵슬롯 데이터베이스에서 인덱스의 장비를 소환, 장비 그랩
        equipItem = QuickSlotsDatabase.Instance.getQuickslotsItem(index);
        equipObj = Instantiate(equipItem.EquipPrefab, gameObject.transform.position, gameObject.transform.rotation);
        
        interactionManager.SelectEnter(RightGrabController.GetComponent<XRDirectInteractor>(), equipObj.GetComponent<XRGrabInteractable>());

        if(equipItem is MeleeWeaponItem)
        {
            equipObj.GetComponent<MeleeWeaponControl>().damage = ((MeleeWeaponItem)equipItem).weaponData.Damage;
        }

        if(equipItem is RangedWeaponItem)
        {
            equipObj.GetComponent<RangedWeaponControl>().damage = ((RangedWeaponItem)equipItem).weaponData.Damage;
            equipObj.GetComponent<RangedWeaponControl>().range = ((RangedWeaponItem)equipItem).rangeWeaponData.Range;
            equipObj.GetComponent<RangedWeaponControl>().m_speed = ((RangedWeaponItem)equipItem).rangeWeaponData.ProjSpeed;
            equipObj.GetComponent<RangedWeaponControl>().shootDelay = ((RangedWeaponItem)equipItem).rangeWeaponData.ShootDelay;
        } 

        if(equipItem is ToolItem)
        {
            equipObj.GetComponent<RepairToolControl>().value = ((ToolItem)equipItem).toolData.Value;
        }    
    }
}
