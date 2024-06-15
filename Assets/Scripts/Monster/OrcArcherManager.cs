using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcArcherManager : OrcManager
{
    public GameObject shootingPoint;
    public GameObject arrow;
    public float m_InitialAngle = 30f;
    private Rigidbody m_Rigidbody;
    public float shootDelay;
    public override IEnumerator Attacking() //벽 혹은 타워를 공격중일 때 호출
    {
        while(true)
        {
            if(encounteredStructure == null)
            {
                m_animator.SetBool("IsAttack", false);
                isAttack = false;
                if(nexusPoint != null)
                {
                    transform.LookAt(nexusPoint.transform.position); // 몬스터가 다시 넥서스를 바라보도록 함
                }
                else
                    Debug.Log("Error : nexusPoint is null");
                coroutineStarted = false; // 다시 벽 혹은 타워를 만날 수 있도록 코루틴 스타트를 false로 바꿔줌
                encounteredStructure = null; // 다음에 만난 벽 혹은 타워    를 저장할 수 있도록 null로 바꿔줌
                //structureManager = null; // 다음에 만난 벽 혹은 타워를 저장할 수 있도록 null로 바꿔줌
                yield break; //코루틴 종료
            }
            m_animator.SetTrigger("Shoot");
            yield return new WaitForSeconds(shootDelay);
            if(encounteredStructure == null)
            {
                m_animator.SetBool("IsAttack", false);
                isAttack = false;
                if(nexusPoint != null)
                {
                    transform.LookAt(nexusPoint.transform.position); // 몬스터가 다시 넥서스를 바라보도록 함
                }
                else
                    Debug.Log("Error : nexusPoint is null");
                coroutineStarted = false; // 다시 벽 혹은 타워를 만날 수 있도록 코루틴 스타트를 false로 바꿔줌
                encounteredStructure = null; // 다음에 만난 벽 혹은 타워    를 저장할 수 있도록 null로 바꿔줌
                //structureManager = null; // 다음에 만난 벽 혹은 타워를 저장할 수 있도록 null로 바꿔줌
                yield break; //코루틴 종료
            }
            //화살 만들어서 날리는 코드
            GameObject _arrow = Instantiate(arrow, shootingPoint.transform.position, shootingPoint.transform.rotation);
            m_Rigidbody = _arrow.GetComponent<Rigidbody>();
            m_Rigidbody.velocity = GetVelocity(shootingPoint.transform.position, encounteredStructure.transform.position, m_InitialAngle);

            Debug.Log("arrow");
            yield return new WaitForSeconds(attackTime);
        }
    }

    public Vector3 GetVelocity(Vector3 player, Vector3 target, float initialAngle)
    {
        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(target.x, 0, target.z);
        Vector3 planarPosition = new Vector3(player.x, 0, player.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = player.y - target.y;

        float initialVelocity
            = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity
            = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects
            = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (target.x > player.x ? 1 : -1);
        Vector3 finalVelocity
            = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        return finalVelocity;
    }
}
