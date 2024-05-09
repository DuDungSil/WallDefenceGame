using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolItem : EquipmentItem
{
    public ToolItemData weaponData { get; private set; }
    public ToolItem(ToolItemData data) : base(data) 
    { 
        weaponData = data;
    }
    public float Value => weaponData.Value;
}
