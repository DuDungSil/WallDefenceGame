using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcWarriorManager : OrcManager
{
    public override void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Structure")) //벽 혹은 타워 레이어와 부딪혔을 경우 (AttackRange가 부딪힘)
        {
            if(!coroutineStarted) //코루틴 실행중이면 안함
            {
                Vector3 structurePosition = other.transform.position; 
                transform.LookAt(structurePosition); //벽 혹은 타워를 바라보도록 몬스터 회전
                m_animator.SetBool("IsAttack", true); //공격 애니메이션 작동
                m_animator.applyRootMotion = true; //얘는 공격애니메이션이 applyRootMotion이 켜져있어야 제대로 작동함.
                isAttack = true;
                encounteredStructure = other.gameObject; // 트리거가 발동된 벽 혹은 타워를 wallOrTower에 저장
                //structureManager = encounteredWallOrTower.GetComponent<StructureManager>();
                attackCoroutine = StartCoroutine(Attacking()); //공격 코루틴 실행
                coroutineStarted = true;
            }
            else if(coroutineStarted && encounteredStructure == null) //코루틴이 실행중인데 encounteredStructure == null이 된 경우 (structure가 업그레이드시 필요)
            {
                if (attackCoroutine != null)
                {
                    StopCoroutine(attackCoroutine);
                    coroutineStarted = false;
                }
                m_animator.SetBool("IsAttack", false);
                isAttack = false;

                Vector3 structurePosition = other.transform.position;
                transform.LookAt(structurePosition); //벽 혹은 타워를 바라보도록 몬스터 회전
                m_animator.SetBool("IsAttack", true); //공격 애니메이션 작동
                m_animator.applyRootMotion = true;
                isAttack = true;
                encounteredStructure = other.gameObject; // 트리거가 발동된 벽 혹은 타워를 wallOrTower에 저장
                //structureManager = encounteredWallOrTower.GetComponent<StructureManager>();
                attackCoroutine = StartCoroutine(Attacking()); //공격 코루틴 실행
                coroutineStarted = true;
            }
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) // 오크가 플레이어의 weapon에 피격당하는 경우
        {
            // 플레이어의 무기 데미지를 받아오는과정 (자식에게 collider가, 부모에게 script가 달려있음)
            ProjectileControl projectileControl = other.gameObject.transform.root.GetComponent<ProjectileControl>();
            if(projectileControl != null)
            {
                float damage = projectileControl.GetDamage();
                TakeDamage(damage);
            }
            else
            {
                Debug.Log("Error : PlayerProjectileControl is null");
            }
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("PlayerMeleeWeapon"))
        {
            MeleeWeaponControl meleeWeaponControl = other.GetComponentInParent<MeleeWeaponControl>();
            if(meleeWeaponControl.Attack())
            {
                Debug.Log("오크가 근접 공격 당함");
                TakeDamage(meleeWeaponControl.damage);
            }     
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("TowerProjectile"))
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
                Debug.Log("Error : TowerProjectileControl is null");
            }
        }
    }
    public override IEnumerator Attacking() //벽 혹은 타워를 공격중일 때 호출
    {
        while(true)
        {
            if(encounteredStructure == null)
            {
                m_animator.SetBool("IsAttack", false);
                m_animator.applyRootMotion = false;
                isAttack = false;
                yield return new WaitForSeconds(attackTime); //AttackTime이 지난 후 다시 앞으로 걸어가도록 하려고했는데 자연스럽지 않음. 변경해야함
                if(nexusPoint != null)
                {
                    transform.LookAt(nexusPoint.transform.position); // 몬스터가 다시 넥서스를 바라보도록 함
                }
                else
                    Debug.Log("Error : nexusPoint is null");
                coroutineStarted = false; // 다시 벽 혹은 타워를 만날 수 있도록 코루틴 스타트를 false로 바꿔줌
                encounteredStructure = null; // 다음에 만난 벽 혹은 타워를 저장할 수 있도록 null로 바꿔줌
                //structureManager = null; // 다음에 만난 벽 혹은 타워를 저장할 수 있도록 null로 바꿔줌
                yield break; //코루틴 종료
            }
            yield return new WaitForSeconds(attackTime);
        }
    }
}
