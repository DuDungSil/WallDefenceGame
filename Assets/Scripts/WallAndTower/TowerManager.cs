using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : StructureManager
{
    public GameObject launchingPad; //발사대
    public GameObject projectile; // 발사체
    protected GameObject target;
    public float shootDelay;
    public float m_projectileSpeed;
    private bool shootActivate = false;
    protected Queue<GameObject> monsterQueue = new Queue<GameObject>();

    private void Update() 
    {
        if(!shootActivate) //shootActivate 가 false일 경우(공격하고 있지 않은 경우)
        {
            if(monsterQueue.Count > 0)
            {
                target = monsterQueue.Dequeue();
                if(target != null)
                {
                    shootActivate = true;
                    StartCoroutine(Shoot(target));
                }
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other) {
        Debug.Log("몬스터 출현");
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            AddMonsterToQueue(other.gameObject);
        } 
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Debug.Log("몬스터 나가쓰요");
            RemoveMonsterFromQueue(other.gameObject);
        }
    }
    protected void AddMonsterToQueue(GameObject monster) // monster를 큐에 넣는 함수
    {
        monsterQueue.Enqueue(monster);
    }
    protected void RemoveMonsterFromQueue(GameObject monster) 
    {
        if(monster == target)
        {
            target = null;
            shootActivate = false;
        }
        else
        {
            Queue<GameObject> tempQueue = new Queue<GameObject>(monsterQueue);
            monsterQueue.Clear();
            while (tempQueue.Count > 0)
            {
                GameObject m = tempQueue.Dequeue();
                if (m != monster && m != null)
                {
                    monsterQueue.Enqueue(m);
                }
            }
        }
    }
    public IEnumerator Shoot(GameObject target)
    {
        while(shootActivate)
        {
            // 투사체 소환
            GameObject spawnedProjectile = Instantiate(projectile, launchingPad.transform.position, launchingPad.transform.rotation);
            //투사체 방향 설정
            Vector3 monsterPos = target.transform.position;
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
            
            if(target == null || target.GetComponent<MonsterManager>().IsDeath == true)
            {
                shootActivate = false;
            }
        }
    }
}
