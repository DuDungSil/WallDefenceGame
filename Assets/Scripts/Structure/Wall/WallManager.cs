using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR.Features.Interactions;

public class WallManager : StructureManager
{
    protected override void Start()
    {
        base.Start();
    }
    public virtual void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("MonsterWeapon"))
        {
            Debug.Log("공격받는중");
            MonsterWeaponDamage monsterWeaponDamage = other.GetComponent<MonsterWeaponDamage>();
            TakeDamage(monsterWeaponDamage.m_damage);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("MonsterProjectile"))
        {
            MonsterWeaponDamage monsterWeaponDamage = other.gameObject.transform.root.GetComponent<MonsterWeaponDamage>();
            TakeDamage(monsterWeaponDamage.m_damage);
        }
    }
    public void temp()
    {
        Debug.Log("hi");
    }
}
