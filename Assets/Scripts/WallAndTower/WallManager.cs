using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR.Features.Interactions;

public class WallManager : StructureManager
{
    public virtual void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("MonsterWeapon"))
        {
            MonsterWeaponDamage monsterWeaponDamage = other.GetComponent<MonsterWeaponDamage>();
            TakeDamage(monsterWeaponDamage.m_damage);
        }
    }
}
