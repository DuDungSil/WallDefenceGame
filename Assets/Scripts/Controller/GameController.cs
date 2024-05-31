using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public ResourceItemData wood;
    public ResourceItemData stone;
    public ResourceItemData iron;
    public MeleeWeaponItemData sword;
    public RangedWeaponItemData staff;
    public ToolItemData hammer;

    [SerializeField]
    private ResourceDatabase rdb;

    private QuickSlotsDatabase qdb;

    // Start is called before the first frame update
    void Start()
    {
        rdb = ResourceDatabase.Instance;
        qdb = QuickSlotsDatabase.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("f"))
        {
            rdb.AddItem((ResourceItem)wood.CreateItem());
        }
        if(Input.GetKeyDown("g"))
        {
            rdb.AddItem((ResourceItem)stone.CreateItem());
        }
        if(Input.GetKeyDown("h"))
        {
            rdb.AddItem((ResourceItem)iron.CreateItem());
        }
        
    }
    public void GetResource(string resourceName)
    {
        if(resourceName == "wood")
            rdb.AddItem((ResourceItem)wood.CreateItem());
        else if(resourceName == "stone")
            rdb.AddItem((ResourceItem)stone.CreateItem());
        else if (resourceName == "iron")
            rdb.AddItem((ResourceItem)iron.CreateItem());
    }
}
