using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    private List<Collider> colliderList = new List<Collider>(); // 충돌한 오브젝트들 저장할 리스트

    [SerializeField]
    private int layerGround = 6; // 지형 레이어 (무시하게 할 것)
    private const int IGNORE_RAYCAST_LAYER = 2;  // ignore_raycast (무시하게 할 것)

    [SerializeField]
    private Material green;
    [SerializeField]
    private Material red;


    void Update()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (colliderList.Count > 0)
        {
            SetColor(red);
        }
        else
        {
            SetColor(green);
        }

    }

    private void SetColorRecursive(Transform parent, Material mat)
    {
        // 부모의 자식들을 모두 순회합니다.
        foreach (Transform tf_Child in parent)
        {
            Renderer renderer = tf_Child.GetComponent<Renderer>();

            if (renderer != null)
            {
                Material[] newMaterials = new Material[renderer.materials.Length];

                for (int i = 0; i < newMaterials.Length; i++)
                {
                    newMaterials[i] = mat;
                }

                renderer.materials = newMaterials;
            }

            // 하위 오브젝트가 있으면 재귀적으로 호출하여 그 하위 오브젝트의 자식들도 모두 순회합니다.
            if (tf_Child.childCount > 0)
            {
                SetColorRecursive(tf_Child, mat);
            }
        }
    }

    private void SetColor(Material mat)
    {
        SetColorRecursive(this.transform, mat);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
            colliderList.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_RAYCAST_LAYER)
            colliderList.Remove(other);
    }

    public bool isBuildable()
    {
        return colliderList.Count == 0;
    }
}