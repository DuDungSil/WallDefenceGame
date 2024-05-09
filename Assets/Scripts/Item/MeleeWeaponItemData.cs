using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_MeleeWeapon_", menuName = "New Item/Item Data/MeleeWeapon")]
public class MeleeWeaponItemData : WeaponItemData
{
    public override Item CreateItem()
    {
        return new MeleeWeaponItem(this);
    }
}
