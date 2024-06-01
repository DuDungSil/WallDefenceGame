using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwicherSlot : MonoBehaviour
{

    GameObject displayItem;
    private EquipmentItem equipitem;

    [Range(0,3)]
    public int index;
    [Range(0f,1f)]
    public float transparency;

    void Start()
    {
        equipitem = QuickSlotsDatabase.Instance.getQuickslotsItem(index);

        if(equipitem != null)
        {
            GameObject displayItem = Instantiate(equipitem.EquipPrefab, gameObject.transform.position, Quaternion.identity);
            DisableAllComponentsExceptMesh(displayItem);
            displayItem.transform.parent = gameObject.transform;
            displayItem.transform.localScale = new Vector3(1f, 1f, 1f);
            displayItem.transform.rotation = gameObject.transform.rotation;
        }

        // 큐브 반투명화.
        Material material = GetComponent<Renderer>().material;
        Color color = material.color;
        color.a = transparency;
        material.color = color;

    }

    void Update()
    {
        
    }
    
     void OnTriggerEnter(Collider collision)
     {
        if(collision.transform.CompareTag("RightHand"))
        {
            if(equipitem != null) HandController.Instance.mountingEquipment(index);
            UIController.Instance.CloseQuick();
        }
     }

    // 메쉬 필터, 렌더러 제외한 컴포넌트,스크립트 제거하는 함수
    void DisableAllComponentsExceptMesh(GameObject obj)
    {
        // 메쉬 필터와 메쉬 렌더러를 가져옵니다.
        MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();

        // 게임 오브젝트에 부착된 모든 컴포넌트를 가져옵니다.
        Component[] components = obj.GetComponents<Component>();

        // 메쉬 필터와 메쉬 렌더러를 제외한 모든 컴포넌트를 비활성화합니다.
        foreach (var component in components)
        {
            if (component != meshFilter && component != meshRenderer && !(component is Transform))
            {
                // 스크립트 컴포넌트도 비활성화합니다.
                if (component is MonoBehaviour)
                {
                    ((MonoBehaviour)component).enabled = false;
                }
                // 그 외의 컴포넌트를 비활성화합니다.
                else if (component is Collider)
                {
                    ((Collider)component).enabled = false;
                }
                else if (component is Renderer)
                {
                    ((Renderer)component).enabled = false;
                }
                else if (component is Rigidbody)
                {
                    ((Rigidbody)component).isKinematic = true;
                }
                else
                {
                    Debug.LogWarning("Unhandled component type: " + component.GetType().ToString());
                }
            }
        }
    }

    // 프리팹 소환 (프리팹의 스크립트 제거? EX.총, 충돌제거, 잡아지는 문제)
    //  NULL 이면 소환 X

    // 엔터 이벤트? => 손에 있는 무기 바꾸기 && 스위처 제거
    //  오른손하고만 충돌
}
