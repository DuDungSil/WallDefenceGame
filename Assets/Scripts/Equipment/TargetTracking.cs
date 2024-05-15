using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTracking : MonoBehaviour
{

    public GameObject Target;
    public GameObject attachPos;

    private Vector3 pos;

    void Start()
    {
        
    }

    void Update()
    {
       if(Target != null)
       {
            // 추적
            if(attachPos != null)
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
            gameObject.transform.rotation = Target.transform.rotation;
            gameObject.transform.position = Target.transform.position - pos;
       } 
    }
}
