using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcArcherManager : OrcManager
{
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
                encounteredStructure = null; // 다음에 만난 벽 혹은 타워를 저장할 수 있도록 null로 바꿔줌
                //structureManager = null; // 다음에 만난 벽 혹은 타워를 저장할 수 있도록 null로 바꿔줌
                yield break; //코루틴 종료
            }
            m_animator.SetTrigger("Shoot");
            yield return new WaitForSeconds(shootDelay);
            //화살 만들어서 날리는 코드
            Debug.Log("arrow");
            yield return new WaitForSeconds(attackTime);
        }
    }
}
