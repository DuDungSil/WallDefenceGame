using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceItem : CountableItem
{
    public ResourceItem(ResourceItemData data, int amount = 1) : base(data, amount) { }
    protected override CountableItem Clone(int amount)
    {
        return new ResourceItem(CountableData as ResourceItemData, amount);
    }
}
