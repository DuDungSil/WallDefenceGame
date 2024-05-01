using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Weapon_", menuName = "New Item/Item Data/Weapon")]
public class WeaponItemData : ItemData
{
    public float Damage => _damage;
    [SerializeField] private float _damage;
    public override Item CreateItem()
    {
        return new WeaponItem(this);
    }
}
