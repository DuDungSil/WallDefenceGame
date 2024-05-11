using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OrcManager : MonsterManager
{
    public int m_armor;
    public override void TakeDamage(int damage)
    {
        if(m_armor < damage) // 방어력이 데미지보다 큰경우는 제외
        {
            Hp = Hp - damage + m_armor;
            if(Hp < 0)
            {
                StartCoroutine(Death());
            }
        }
    }
    public override void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            m_animator.SetBool("IsAttack", true);
            WallManager wallManager = other.GetComponent<WallManager>();
            if(wallManager != null)
            {
                if(wallManager.Hp < 0)
                {
                    m_animator.SetBool("IsAttack", false);
                }
            }
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Arrow"))
        {
            Debug.Log("오크가 화살에 맞았다.");
            WeaponDamage weaponDamage = other.GetComponent<WeaponDamage>(); // 플레이어의 무기 데미지를 받아오는것.
            if(weaponDamage != null)
            {
                int damage = weaponDamage.m_damage;
                TakeDamage(damage);
                Debug.Log("currentHp : " + Hp);
            }
        }
    }
}
