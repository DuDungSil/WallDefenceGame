using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Tool_", menuName = "New Item/Item Data/Tool")]
public class ToolItemData : EquipmentItemData
{
    public float Value => _value;
    [SerializeField] private float _value;
    public override Item CreateItem()
    {
        return new ToolItem(this);
    }
}
