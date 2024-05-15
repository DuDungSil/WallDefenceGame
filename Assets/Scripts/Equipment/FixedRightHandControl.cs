using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRightHandControl : MonoBehaviour
{
    EquipmentItem grabitem;
    GameObject grabobj;

    public GameObject grabPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setEquipment(int index)
    {
        // 원래 장비 삭제
        if(grabobj != null)
        {
            Destroy(grabobj);
        }

        //퀵슬롯 데이터베이스에서 인덱스의 장비를 소환, 장비 타겟 설정
        grabitem = QuickSlotsDatabase.Instance.getQuickslotsItem(index);
        grabobj = Instantiate(grabitem.EquipPrefab, gameObject.transform.position, gameObject.transform.rotation);
        grabobj.GetComponent<TargetTracking>().Target = grabPos;
        grabobj.transform.parent = grabPos.transform; 

        if(grabitem is MeleeWeaponItem)
        {
            grabobj.GetComponent<MeleeWeaponControl>().damage = ((MeleeWeaponItem)grabitem).weaponData.Damage;
        }

        if(grabitem is RangedWeaponItem)
        {
            grabobj.GetComponent<RangedWeaponControl>().projectilePrefab = ((RangedWeaponItem)grabitem).rangeWeaponData.ProjectilePrefab;
            grabobj.GetComponent<RangedWeaponControl>().damage = ((RangedWeaponItem)grabitem).weaponData.Damage;
            grabobj.GetComponent<RangedWeaponControl>().range = ((RangedWeaponItem)grabitem).rangeWeaponData.Range;
            grabobj.GetComponent<RangedWeaponControl>().m_speed = ((RangedWeaponItem)grabitem).rangeWeaponData.ProjSpeed;
            grabobj.GetComponent<RangedWeaponControl>().shootDelay = ((RangedWeaponItem)grabitem).rangeWeaponData.ShootDelay;
        } 

        if(grabitem is ToolItem)
        {
            grabobj.GetComponent<RepairToolControl>().value = ((ToolItem)grabitem).toolData.Value;
        }      
    }
}
