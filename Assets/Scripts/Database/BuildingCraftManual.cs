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

[System.Serializable]
public class BuildingBOM
{
    public BuildingBOMData Data;

    [HideInInspector] 
    public ResourceItem[] craftNeedItems;  // 필요한 아이템

    public void Init()
    {
        craftNeedItems = new ResourceItem[Data.craftNeedItemDatas.Length];
        for(int i = 0; i < Data.craftNeedItemDatas.Length; i++)
        {
            craftNeedItems[i] = (ResourceItem)Data.craftNeedItemDatas[i].CreateItem();
        }
    }
}

public class BuildingCraftManual : Singleton<BuildingCraftManual>
{
    [SerializeField]
    private BuildingBOM[] wall_Manual;  // 벽 메뉴얼 

    [SerializeField]
    private BuildingBOM[] tower_Manual;  // 타워 메뉴얼 

    [SerializeField]
    private BuildingBOM[] house_Manual;  // 하우스 메뉴얼

    public BuildingBOM[] getWallManual()
    {
        return wall_Manual;
    }

    public BuildingBOM[] getTowerManual()
    {
        return tower_Manual;
    } 

    public BuildingBOM[] getHouseManual()
    {
        return house_Manual;
    } 
    
    void Start()
    {
        foreach(var bom in  wall_Manual) bom.Init();
        foreach(var bom in  tower_Manual) bom.Init();
        foreach(var bom in  house_Manual) bom.Init();
    }

    
    void Update()
    {
        
    }
}
