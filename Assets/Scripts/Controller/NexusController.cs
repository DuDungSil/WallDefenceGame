using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusController : Singleton<NexusController>
{
    public GameObject[] attackPoint;
    [SerializeField]
    protected float hp;
    public float Hp
    {
        get { return hp; }
        private set { hp = value; }
    }
    public void TakeDamage(float damage)
    {
        Hp = Hp - damage;
        Debug.Log(Hp);
        if (Hp < 0)
        {
            //GameOver시 수행할 코드
            Debug.Log("GameOver");
        }
    }
    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("MonsterWeapon"))
        {
            MonsterWeaponDamage monsterWeaponDamage = other.GetComponent<MonsterWeaponDamage>();
            TakeDamage(monsterWeaponDamage.m_damage);
        }
    }
}
