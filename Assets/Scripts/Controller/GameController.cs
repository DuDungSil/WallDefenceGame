using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public ResourceItemData wood;
    public ResourceItemData stone;
    public ResourceItemData iron;
    public ToolItemData[] starterKit;

    [SerializeField]
    private ResourceDatabase rdb;

    // Start is called before the first frame update
    void Start()
    {
        rdb = ResourceDatabase.Instance;
        for(int i = 0; i < starterKit.Length; i++)
        {
            EquipmentDatabase.Instance.AddItem((EquipmentItem)starterKit[i].CreateItem(), 3);
        }
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

}
