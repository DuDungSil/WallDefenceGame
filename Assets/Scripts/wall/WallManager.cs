using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR.Features.Interactions;

public class WallManager : MonoBehaviour
{
    [SerializeField]
    private int hp;
    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    public virtual void TakeDamage(int damage)
    {
        Hp = Hp - damage;
        if (Hp < 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("MonsterWeapon"))
        {
            MonsterWeaponDamage monsterWeaponDamage = other.GetComponent<MonsterWeaponDamage>();
            TakeDamage(monsterWeaponDamage.m_damage);
        }
    }
}
