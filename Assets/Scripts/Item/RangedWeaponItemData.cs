using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_RangedWeapon_", menuName = "New Item/Item Data/RangedWeapon")]
public class RangedWeaponItemData : WeaponItemData
{
    public float Range => _range;
    public float ProjSpeed => _projSpeed;
    public float ShootDelay => _shootDelay;
    
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _range;
    [SerializeField] private float _projSpeed;
    [SerializeField] private float _shootDelay;

    public override Item CreateItem()
    {
        return new RangedWeaponItem(this);
    }
}
