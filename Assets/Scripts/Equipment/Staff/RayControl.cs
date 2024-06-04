using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayControl : MonoBehaviour
{
    public float rayDistance = 100f; // 레이캐스트의 거리
    private LineRenderer lineRenderer;

    private RaycastHit hit;  // 레이캐스트 충돌 정보
    private int layerMask;

    void Start()
    {
        // LineRenderer 컴포넌트를 가져옴
        lineRenderer = GetComponent<LineRenderer>();
        layerMask = (1 << 3) | (1 << 6);
    }

    void Update()
    {
        // 오브젝트의 위치와 forward 방향
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;



        // 레이캐스트를 쏴서 충돌 여부 확인
        if (Physics.Raycast(origin, direction, out hit, rayDistance, layerMask))
        {
            // 충돌 지점의 좌표 설정
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, hit.point);

            //Debug.Log("Hit point: " + hit.point);
        }
        else
        {
            // 충돌하지 않은 경우, 레이를 최대 거리까지 그리기
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, origin + direction * rayDistance);

            //Debug.Log("No collision detected.");
        }
    }

    public Vector3 getRayHitPoint()
    {
        return hit.point;
    }
}
