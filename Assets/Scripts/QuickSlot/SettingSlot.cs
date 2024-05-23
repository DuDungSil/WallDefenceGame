using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingSlot : MonoBehaviour
{
    GameObject displayItem;
    public float widthSize = 2;
    public float heightSize = 2;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // 슬롯 업데이트 함수          슬롯 아이템 위치 밑으로 계산 ?
    public void SlotUpadate(GameObject prefab)
    {
        if(displayItem) Destroy(displayItem);
        displayItem = Instantiate(prefab, gameObject.transform.position, gameObject.transform.rotation);

        displayItem.transform.parent = gameObject.transform;

        BoxCollider collider = GetComponent<BoxCollider>();
        float yLength = collider.size.y;
        Vector3 currentPosition = displayItem.transform.position;
        currentPosition.y -= 0.3f * yLength;
        displayItem.transform.position = currentPosition;

        Vector3 Scale = displayItem.transform.localScale;
        displayItem.transform.localScale = new Vector3(Scale.x * widthSize, Scale.y * heightSize, Scale.z);

        DisableAllComponentsExceptMesh(displayItem);
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
}
