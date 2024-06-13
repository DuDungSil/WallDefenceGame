using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponControl : MonoBehaviour
{
    public float damage;
    public float coolTime;
    public float magnitudeThreshold;

    private float magnitude; // 속도의 크기
    private bool isCoolTime = false;
    private Vector3 previousPosition;
    private float deltaTime;

    void FixedUpdate()
    {
        // 현재 위치 계산
        Vector3 currentPosition = transform.position;

        // 프레임 간 시간 간격 계산
        deltaTime = Time.deltaTime;

        // 속도 계산 (변위 / 시간 간격)
        Vector3 velocity = (currentPosition - previousPosition) / deltaTime;

        // 이전 위치 업데이트
        previousPosition = currentPosition;

        magnitude = velocity.magnitude;

    }
    public bool Attack()
    {
        if(magnitude > magnitudeThreshold && !isCoolTime)
        {
            isCoolTime = true;
            StartCoroutine(ActivateCooltime());
            return true;
        }
        else
        {
            return false;
        }
    }

    protected IEnumerator ActivateCooltime()
    {
        yield return new WaitForSeconds(coolTime);
        isCoolTime = false;
    }

}
