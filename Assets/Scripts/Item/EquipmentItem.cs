using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentItem : Item
{
    public EquipmentItemData equipmentData { get; private set; }

    public GameObject EquipPrefab => equipmentData.EquipPrefab;

    public GameObject QuickSlotPrefab => equipmentData.QuickSlotPrefab;

    public int HandIndex => equipmentData.HandIndex;

    public EquipmentItem(EquipmentItemData data) : base(data)
    {
        equipmentData = data;
    }

}
