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
    private int quickIndex;


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
    public void mountingEquipment(int index)
    {
        DeleteEquipObject();
        setEquipment(index);
    }

    // 소환했던 장비 오브젝트 삭 제
    public void DeleteEquipObject()
    {
        if(equipObj != null)
        {
            if(equipItem is DistanceWeaponItem)
            {
                // 장착중인 장비의 쿨타임, 탄환 수 저장하는 코드 추가 필요
                DistanceWeanponControl rangedWeaponControl = equipObj.GetComponent<DistanceWeanponControl>();
                ((DistanceWeaponItem)equipItem).remainAmmo = rangedWeaponControl.remainAmmo;
                ((DistanceWeaponItem)equipItem).lastShootTime = rangedWeaponControl.lastShootTime;
                ((DistanceWeaponItem)equipItem).lastTime = rangedWeaponControl.lastCoolTime;
                ((DistanceWeaponItem)equipItem).isCoolTime = rangedWeaponControl.isCoolTime;
                QuickSlotsDatabase.Instance.saveQuickslotsItem(quickIndex, equipItem);
            }



            // HUD를 끄는 코드 추가 필요 (equipObj객체 정보를 넘겨줌)
            Destroy(equipObj);
        }
    }


    public void setEquipment(int index)
    {
        //퀵슬롯 데이터베이스에서 인덱스의 장비를 소환, 장비 그랩
        equipItem = QuickSlotsDatabase.Instance.getQuickslotsItem(index);
        equipObj = Instantiate(equipItem.EquipPrefab, gameObject.transform.position, gameObject.transform.rotation);
        quickIndex = index;

        if(equipItem.equipmentData.HandIndex == 0)
        {
            interactionManager.SelectEnter(LeftGrabController.GetComponent<XRDirectInteractor>(), equipObj.GetComponent<XRGrabInteractable>());
        }
        else if (equipItem.equipmentData.HandIndex == 1)
        {
            interactionManager.SelectEnter(RightGrabController.GetComponent<XRDirectInteractor>(), equipObj.GetComponent<XRGrabInteractable>());
        }

        if(equipItem is MeleeWeaponItem)
        {

        }

        if(equipItem is DistanceWeaponItem)
        {
            // 장착중인 장비의 쿨타임, 탄환 수 불러오는 코드 추가 필요
            DistanceWeanponControl ditanceWeaponControl = equipObj.GetComponent<DistanceWeanponControl>();
            ditanceWeaponControl.lastShootTime = ((DistanceWeaponItem)equipItem).lastShootTime;
            ditanceWeaponControl.isCoolTime = ((DistanceWeaponItem)equipItem).isCoolTime;
            ditanceWeaponControl.LoadData(((DistanceWeaponItem)equipItem).lastTime, ((DistanceWeaponItem)equipItem).remainAmmo);
        }

        if(equipItem is ToolItem)
        {

        }    

        // HUD를 키는 코드 추가 필요 (equipObj객체 정보를 넘겨줌)
    }
}
