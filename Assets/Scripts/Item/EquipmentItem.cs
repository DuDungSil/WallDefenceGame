using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentItem : Item
{
    public EquipmentItemData equipmentData { get; private set; }

    public GameObject prefab => equipmentData.Prefab;

    public EquipmentItem(EquipmentItemData data) : base(data)
    {
        equipmentData = data;
    }

}
