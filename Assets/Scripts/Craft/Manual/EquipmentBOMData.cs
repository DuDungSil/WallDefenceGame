using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentBOM_", menuName = "New BOM/Equipment BOM")]
public class EquipmentBOMData : ScriptableObject
{
    public string craftName;
    public EquipmentItemData itemdata;
    public ResourceItemData[] craftNeedItemDatas; 
    public int[] craftNeedItemCount; // 필요한 아이템의 개수
}
