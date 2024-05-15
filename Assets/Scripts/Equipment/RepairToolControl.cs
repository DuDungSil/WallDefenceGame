using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairToolControl : MonoBehaviour
{

    public float value;
    public float magnitude; // 속도의 크기

    private Vector3 previousPosition;
    private float deltaTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    // 벽이나 타워 수리
    // value값 필요 ( 얼마나 수리될지 )
    // 속도값 계산 필요
}
