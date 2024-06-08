using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.OpenXR.Features.Interactions;


public abstract class MonsterManager : MonoBehaviour
{
    public CharacterController characterController;
    public float speed;
    public bool isAttack;
    public GameObject nexusPoint;
    [SerializeField]
    protected float hp;
    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    //public StructureManager structureManager;
    public bool IsDeath = false;
    protected bool coroutineStarted = false; // Attacking 코루틴 제어용
    [SerializeField]
    protected float deathTime = 5f;
    [SerializeField]
    protected float attackTime = 2.267f;
    [SerializeField]
    protected Vector3 nexusPosition; // 넥서스 위치 (플레이어의 넥서스)
    protected GameObject encounteredWallOrTower; //몬스터의 사거리 내에 들어온 벽 혹은 타워
    //protected StructureManager structureManager;
    public Animator m_animator;

    public IEnumerator Attacking() //벽 혹은 타워를 공격중일 때 호출
    {
        while(true)
        {
            if(encounteredWallOrTower == null)
            {
                m_animator.SetBool("IsAttack", false);
                isAttack = false;
                yield return new WaitForSeconds(attackTime); //AttackTime이 지난 후 다시 앞으로 걸어가도록 하려고했는데 자연스럽지 않음. 변경해야함
                if(nexusPoint != null)
                {
                    transform.LookAt(nexusPoint.transform.position); // 몬스터가 다시 넥서스를 바라보도록 함
                }
                else
                    Debug.Log("Error : nexusPoint is null");
                coroutineStarted = false; // 다시 벽 혹은 타워를 만날 수 있도록 코루틴 스타트를 false로 바꿔줌
                encounteredWallOrTower = null; // 다음에 만난 벽 혹은 타워를 저장할 수 있도록 null로 바꿔줌
                //structureManager = null; // 다음에 만난 벽 혹은 타워를 저장할 수 있도록 null로 바꿔줌
                yield break; //코루틴 종료
            }
            yield return new WaitForSeconds(attackTime);
        }
    }
    public virtual void TakeDamage(float damage) //몬스터가 공격받았을 때에 데미지를 받는 함수, 하위 클래스에서 몬스터 특성에 따라 오버라이딩 가능
    {
        if(!IsDeath)
        {
            Hp = Hp - damage;
            if (Hp < 0 )
            {
                IsDeath = true;
                StartCoroutine(Death());
            }
        }
    }
    public IEnumerator Death() //몬스터가 죽는 코루틴, 몬스터의 사망 애니메이션의 길이에 따라 deathTime을 조절하면 될듯.
    {
        m_animator.SetTrigger("DeathTrigger");
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
    public abstract void OnTriggerEnter(Collider other); //몬스터의 충돌 처리함수 (attackRange, MonsterWeapon, 몬스터의 characterController등 여러 콜라이더의 충돌을 한꺼번에 관리)

    void Start()
    {
        if(nexusPoint != null)
        {
            transform.LookAt(nexusPoint.transform.position); // 몬스터 생성시 넥서스 위치를 바라보도록 함
        }
        else
            Debug.Log("Error : nexusPoint is null");
    }
    void Update()
    {
        if (!isAttack)
        {
            // 목표 지점을 바라보게 함
            Vector3 direction = nexusPoint.transform.position - transform.position;
            direction.y = 0;  // Y축 방향의 회전은 무시

            if (direction.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * speed);
            }

            // 앞으로 이동
            Vector3 move = transform.forward * speed * Time.deltaTime;
            characterController.Move(move);
        }
    }
}
