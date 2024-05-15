using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponItem : WeaponItem
{    
    public RangedWeaponItemData rangeWeaponData { get; private set; }

    public RangedWeaponItem(RangedWeaponItemData data) : base(data) 
    { 
        rangeWeaponData = data;
    }

    public GameObject ProjectilePrefab => rangeWeaponData.ProjectilePrefab;
    public float Range => rangeWeaponData.Range;
    public float ProjSpeed => rangeWeaponData.ProjSpeed;
    public float ShootDelay => rangeWeaponData.ShootDelay;
}
