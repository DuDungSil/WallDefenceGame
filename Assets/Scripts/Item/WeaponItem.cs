using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponItem : EquipmentItem
{
    public WeaponItemData weaponData { get; private set; }
    public WeaponItem(WeaponItemData data) : base(data) 
    {
        weaponData = data;
    }

    public float Damage => weaponData.Damage;
}
