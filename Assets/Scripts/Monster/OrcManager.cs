using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OrcManager : MonsterManager
{
    protected bool meleeAtackActivate = true;
    public float m_armor;
    public override void TakeDamage(float damage)
    {
        if(m_armor < damage) // 방어력이 데미지보다 큰경우는 제외
        {
            if(!IsDeath)
            {
                Hp = Hp - damage + m_armor;
                Debug.Log(Hp);
                if(Hp <= 0)
                {
                    IsDeath = true;
                    StartCoroutine(Death());
                }
            }
        }
    }


    public override void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Structure")) //벽 혹은 타워 레이어와 부딪혔을 경우 (AttackRange가 부딪힘)
        {
            if(!coroutineStarted) //코루틴 실행중이면 안함
            {
                Vector3 structurePosition = other.transform.position; 
                transform.LookAt(structurePosition); //벽 혹은 타워를 바라보도록 몬스터 회전
                m_animator.SetBool("IsAttack", true); //공격 애니메이션 작동
                isAttack = true;
                encounteredStructure = other.gameObject; // 트리거가 발동된 벽 혹은 타워를 wallOrTower에 저장
                //structureManager = encounteredWallOrTower.GetComponent<StructureManager>();
                StartCoroutine(Attacking()); //공격 코루틴 실행
                coroutineStarted = true;
            }
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) // 오크가 플레이어의 weapon에 피격당하는 경우
        {
            // 플레이어의 무기 데미지를 받아오는과정 (자식에게 collider가, 부모에게 script가 달려있음)
            ProjectileControl projectileControl = other.gameObject.transform.root.GetComponent<ProjectileControl>();
            if(projectileControl != null)
            {
                float damage = projectileControl.damage;
                TakeDamage(damage);
            }
            else
            {
                Debug.Log("Error : PlayerProjectileControl is null");
            }
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("PlayerMeleeWeapon") && meleeAtackActivate )
        {
            meleeAtackActivate = false;
            Debug.Log("오크가 근접 공격 당함");
            float damage = other.GetComponentInParent<MeleeWeaponControl>().damage; // 플레이어의 무기 데미지를 받아오는것.
            float magnitude = other.GetComponentInParent<MeleeWeaponControl>().magnitude;
            if(magnitude > 0.5f)
            {
                TakeDamage(damage);
                Debug.Log(magnitude);
                Debug.Log(Hp);
                StartCoroutine(MeleeAtackDelay());  
            }       
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("TowerProjectile"))
        {
            ProjectileControl projectileControl = other.gameObject.GetComponent<ProjectileControl>();
            if(projectileControl != null)
            {
                float damage = projectileControl.damage;
                TakeDamage(damage);
            }
            else
            {
                Debug.Log("Error : TowerProjectileControl is null");
            }
        }
    }
    protected IEnumerator MeleeAtackDelay()
    {
        yield return new WaitForSeconds(1f);
        meleeAtackActivate = true;
    }
}
