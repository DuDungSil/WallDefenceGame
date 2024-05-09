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
                pos = attachPos.transform.localPosition;
            }
            else
            {
                pos = Vector3.zero;
            }
            gameObject.transform.position = Target.transform.position + pos;
            gameObject.transform.rotation = Target.transform.rotation;
            Debug.Log(gameObject.transform.position);
       } 
    }
}
