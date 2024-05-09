using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_RangedWeapon_", menuName = "New Item/Item Data/RangedWeapon")]
public class RangedWeaponItemData : WeaponItemData
{
    public override Item CreateItem()
    {
        return new RangedWeaponItem(this);
    }
}
