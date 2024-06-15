using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcShamanManager : OrcManager
{
    public GameObject magicCircle;
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
                magicCircle.transform.position = encounteredStructure.transform.position; //마법진 위치를 마주친 structure의 위치로 변경
                magicCircle.SetActive(true); //마법진 소환
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
                Debug.Log("Error : projectileControl is null");
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
    }
    public override IEnumerator Attacking() //벽 혹은 타워를 공격중일 때 호출
    {
        while(true)
        {
            if(encounteredStructure == null)
            {
                m_animator.SetBool("IsAttack", false);
                isAttack = false;
                magicCircle.SetActive(false); //마법진 비활성화
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
            magicCircle.GetComponent<Collider>().enabled = true;
            yield return new WaitForSeconds(0.1f);
            magicCircle.GetComponent<Collider>().enabled = false;
            yield return new WaitForSeconds(attackTime);
            
        }
    }
    public override IEnumerator Death() //몬스터가 죽는 코루틴, 몬스터의 사망 애니메이션의 길이에 따라 deathTime을 조절하면 될듯.
    {
        m_animator.SetTrigger("DeathTrigger");
        SoundController.Instance.PlaySound3D("Monster_death", gameObject.transform);
        yield return new WaitForSeconds(deathTime);
        UIController.Instance.OpenGameClear();
        Destroy(gameObject);
    }
}
