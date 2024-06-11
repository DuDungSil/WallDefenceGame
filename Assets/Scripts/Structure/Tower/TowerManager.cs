

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowerManager : StructureManager
{
    public GameObject shootingPoint; // 투사체 생성 지점
    public GameObject launchingPad; // 발사대 
    public GameObject projectile; // 발사체
    protected GameObject target;
    public float shootDelay;
    public float m_projectileSpeed;
    private float timer;
    protected Queue<GameObject> monsterQueue = new Queue<GameObject>();

    protected override void Start()
    {
        base.Start();
        timer = 0f;
    }
    protected virtual void Update() 
    {
        if(isAssigned)
        {
            if(target == null && monsterQueue.Count < 1)
            {
                return;
            }
            timer += Time.deltaTime;
            if(timer > shootDelay)
            {
                if(target == null)
                {
                    if(monsterQueue.Count > 0)
                    {
                        target = monsterQueue.Dequeue();
                    }
                }
                if(target != null)
                {
                    Shoot(target);
                }
                timer = 0f;
            }
        }
    }

    public override void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Debug.Log("몬스터 출현");
            AddMonsterToQueue(other.gameObject);
        }
        base.OnTriggerEnter(other);
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
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
    public virtual void Shoot(GameObject target)
    {
        //투사체 방향 설정
        Vector3 monsterPos = target.transform.position;
        Vector3 monsterDirection = (monsterPos - shootingPoint.transform.position).normalized;
        launchingPad.transform.rotation = Quaternion.LookRotation(monsterDirection); // 발사대 방향 설정
        // 투사체 소환
        GameObject spawnedProjectile = Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);
        /* 발사체를 일정 속도로 돌리기
        Quaternion rotation = Quaternion.LookRotation(monsterPos);
        launchingPad.transform.rotation = Quaternion.Lerp(launchingPad.transform.rotation, rotation, 10f * Time.deltaTime);
        */
        spawnedProjectile.transform.rotation = Quaternion.LookRotation(monsterDirection); // 발사체 방향 설정
        // 물리속성 설정
        Rigidbody r = spawnedProjectile.GetComponent<Rigidbody>();
        r.AddForce(monsterDirection * m_projectileSpeed, ForceMode.Impulse);
    }
}
