using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public WeaponItemData aa;
    public ResourceItemData bb;
    public Item axe;
    public Item wood;
    [SerializeField]
    private Inventory theInventory;

    // Start is called before the first frame update
    void Start()
    {
        axe = new WeaponItem(aa);
        wood = new ResourceItem(bb);

    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown("f"))
        // {
        //     theInventory.AcquireItem(axe);
        // }
        // if(Input.GetKeyDown("g"))
        // {
        //     theInventory.AcquireItem(wood);
        // }
    }
}
