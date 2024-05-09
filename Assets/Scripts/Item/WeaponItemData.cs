using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponItemData : EquipmentItemData
{
    public float Damage => _damage;
    [SerializeField] private float _damage;
}