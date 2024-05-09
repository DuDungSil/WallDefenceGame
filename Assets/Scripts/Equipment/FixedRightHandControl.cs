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
        grabobj = Instantiate(grabitem.prefab, gameObject.transform.position, gameObject.transform.rotation);
        grabobj.GetComponent<TargetTracking>().Target = grabPos;
        grabobj.transform.parent = grabPos.transform; 
    }
}
