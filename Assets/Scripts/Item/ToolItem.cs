using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolItem : EquipmentItem
{
    public ToolItemData toolData { get; private set; }
    public ToolItem(ToolItemData data) : base(data) 
    { 
        toolData = data;
    }
}
