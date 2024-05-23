using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    private List<Collider> colliderList = new List<Collider>(); // 충돌한 오브젝트들 저장할 리스트

    [SerializeField]
    private int layerGround; // 지형 레이어 (무시하게 할 것)
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

    private void SetColor(Material mat)
    {
        foreach (Transform tf_Child in this.transform)
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
            // renderer가 null인 경우에는 아무 작업도 하지 않습니다.
        }
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