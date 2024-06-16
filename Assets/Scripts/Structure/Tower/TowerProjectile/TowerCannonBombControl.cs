using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCannonBombControl : ProjectileControl
{
    private Rigidbody rb;
    public GameObject bombEffect;
    private Vector3 customGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(DestroyAfterTime());
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Debug.Log("aa");
            GameObject explosion = Instantiate(bombEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Bulidable"))
        {
            Debug.Log("bb");
            GameObject explosion = Instantiate(bombEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Initialize(Vector3 initialVelocity, Vector3 gravity)
    {
        rb.velocity = initialVelocity;
        customGravity = gravity;
        rb.useGravity = false; // 기본 중력 비활성화
    }

    void FixedUpdate()
    {
        // 사용자 정의 중력 적용
        rb.velocity += customGravity * Time.fixedDeltaTime;
    }
}
