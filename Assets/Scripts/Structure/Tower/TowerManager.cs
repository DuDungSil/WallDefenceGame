

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class TowerManager : AssignUnitStructureManager
{
    public GameObject shootingPoint; // 투사체 생성 지점
    public GameObject launchingPad; // 발사대 
    public GameObject projectile; // 발사체
    protected GameObject target;
    public float shootDelay;
    public float m_projectileSpeed;

    private bool shootActivate = true;
    protected Queue<GameObject> monsterQueue = new Queue<GameObject>();

    protected override void Start()
    {
        base.Start();
    }
    protected virtual void Update() 
    {
        if(isAssigned)
        {
            if(shootActivate)
            {
                if(target == null && monsterQueue.Count < 1)
                {
                    return;
                }
                else
                {
                    if(target == null)
                    {
                        if(monsterQueue.Count > 0)
                        {
                            target = monsterQueue.Dequeue();
                        }
                    }
                    else
                    {
                        Shoot(target);
                        shootActivate = false;
                        StartCoroutine(ShootDelay());
                    }
                }
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
    
    public abstract void Shoot(GameObject target);

    protected IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        shootActivate = true;
    }
}
