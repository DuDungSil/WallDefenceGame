using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltCointol : ProjectileControl
{
    // 회전 속도
    public float rotationSpeed = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        // 회전시키기
        // 랜덤한 축을 생성합니다
        Vector3 randomAxis = new Vector3(Random.value, Random.value, Random.value).normalized;

        // 오브젝트를 회전시킵니다
        transform.Rotate(randomAxis, rotationSpeed * Time.deltaTime);
    }
}
