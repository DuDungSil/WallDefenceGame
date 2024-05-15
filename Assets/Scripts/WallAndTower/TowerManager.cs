using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : StructureManager
{
    public GameObject launchingPad; //발사대
    public GameObject projectile; // 발사체
    private GameObject encounteredMonster;
    public float shootDelay;
    public float m_projectileSpeed;
    private bool shootActivate = true;
    public virtual void OnTriggerEnter(Collider other) {
        Debug.Log("몬스터 출현");
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster") && encounteredMonster == null)
        {
            encounteredMonster = other.gameObject;
            StartCoroutine(Shoot(encounteredMonster));
        } 
    }
    IEnumerator Shoot(GameObject _encounteredMonster)
    {
        while(shootActivate)
        {
            // 투사체 소환
            GameObject spawnedProjectile = Instantiate(projectile, launchingPad.transform.position, launchingPad.transform.rotation);
            //투사체 방향 설정
            Vector3 monsterPos = encounteredMonster.transform.position;
            Vector3 monsterDirection = (monsterPos - launchingPad.transform.position).normalized;
            launchingPad.transform.rotation = Quaternion.LookRotation(monsterDirection); // 발사대 방향 설정
            /* 발사체를 일정 속도로 돌리기
            Quaternion rotation = Quaternion.LookRotation(monsterPos);
            launchingPad.transform.rotation = Quaternion.Lerp(launchingPad.transform.rotation, rotation, 10f * Time.deltaTime);
            */
            spawnedProjectile.transform.rotation = Quaternion.LookRotation(monsterDirection); // 발사체 방향 설정
            // 물리속성 설정
            Rigidbody r = spawnedProjectile.GetComponentInChildren<Rigidbody>();
            r.AddForce(monsterDirection * m_projectileSpeed, ForceMode.Impulse);
            yield return new WaitForSeconds(shootDelay);
            if(_encounteredMonster == null || _encounteredMonster.GetComponent<MonsterManager>().IsDeath == true)
            {
                shootActivate = false;
            }
        }
    }
}
