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
            GameObject displayItem = Instantiate(equipitem.QuickSlotPrefab, gameObject.transform);
            //displayItem.transform.localScale = new Vector3(1f, 1f, 1f);
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


}
