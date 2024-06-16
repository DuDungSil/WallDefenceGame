using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltContol : ProjectileControl
{
    // 회전 속도
    public bool isCollision = false;
    public float rotationSpeed = 10f;
    private int maxPierces = 1; // Maximum number of targets the bullet can pierce through
    private int currentPierces = 0; // Current number of targets pierced through

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

    public void SetMaxPierces(int num)
    {
        maxPierces = num;
    }
    public void SetCollision(bool b)
    {
        isCollision = b;
    }

    new void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            if(isCollision) Destroy(gameObject);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            if(isCollision)
            {
                currentPierces++;

                if (currentPierces >= maxPierces)
                {
                    Destroy(gameObject);
                    return;
                }   

            }
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Bulidable"))
        {
            if(isCollision) Destroy(gameObject);
        }  
    }
}
