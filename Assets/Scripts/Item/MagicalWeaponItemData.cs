using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_MagicalWeapon_", menuName = "New Item/Item Data/MagicalWeapon")]
public class MagicalWeaponItemData : DistanceWeaponItemData
{
    public override Item CreateItem()
    {
        return new MagicalWeaponItem(this);
    }
}
