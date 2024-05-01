using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Resource_", menuName = "New Item/Item Data/Resource")]
public class ResourceItemData : CountableItemData
{
    public override Item CreateItem()
    {
        return new ResourceItem(this);
    }
}
