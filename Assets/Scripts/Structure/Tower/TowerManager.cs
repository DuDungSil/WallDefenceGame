

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

    public GameObject assignedMark;

    // 공격 딜레이
    private bool shootActivate = true;
    protected Queue<GameObject> monsterQueue = new Queue<GameObject>();

    protected override void Start()
    {
        base.Start();
        assignedMark.SetActive(!isAssigned);
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
                    if(target == null || target.GetComponent<MonsterManager>().isDeath == true) // target이 죽어도 변경하도록
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

    public override void AssignedChange() //AssignBtn과 RemoveBtn의 이벤트함수
    {
        if(isAssigned) //RemoveBtn listener
        {
            if(UnitController.Instance.RemoveUnits(NeededUnits))
            {
                isAssigned = false;
                if(Units != null)
                {
                    assignedMark.SetActive(true);
                    Units.SetActive(false);
                }
            }
        }
        else //AssignBtn listener
        {
            if(UnitController.Instance.AssignUnits(NeededUnits))
            {
                isAssigned = true;
                if(Units != null)
                {
                    assignedMark.SetActive(false);
                    Units.SetActive(true);
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
