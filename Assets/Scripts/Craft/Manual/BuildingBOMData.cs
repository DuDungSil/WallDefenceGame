using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingBOM_", menuName = "New BOM/Building BOM")]
public class BuildingBOMData : ScriptableObject
{
    public string craftName;
    public Sprite buildingImage;
    public GameObject prefab; // 실제 설치 될 프리팹
    public GameObject PreviewPrefab; // 미리 보기 프리팹
    public ResourceItemData[] craftNeedItemDatas; 
    public int[] craftNeedItemCount;
}