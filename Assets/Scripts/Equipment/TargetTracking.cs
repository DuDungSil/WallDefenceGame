using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTracking : MonoBehaviour
{

    public GameObject Target;
    public GameObject attachPos;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

    if (rb == null)
    {
        Debug.LogError("Rigidbody component is missing.");
    }
    }

    void Update()
    {
       
    }

    void FixedUpdate()
    {
        if (Target != null)
        {
            // 추적
            Vector3 pos;
            if (attachPos != null)
            {
                // attachPos의 로컬 좌표
                Vector3 fromOriginToObject = attachPos.transform.localPosition;

                // 타겟의 rotation 값을 가져옵니다.
                Quaternion rotation = Target.transform.rotation;

                // 벡터를 회전시킵니다.
                pos = rotation * fromOriginToObject;
            }
            else
            {
                pos = Vector3.zero;
            }

            // Rigidbody를 사용하여 위치와 회전을 설정합니다.
            rb.MovePosition(Target.transform.position - pos);
            rb.MoveRotation(Target.transform.rotation);
        }
    }
}
