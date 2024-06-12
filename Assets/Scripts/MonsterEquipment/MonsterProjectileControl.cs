using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectileControl : MonsterWeaponDamage
{
    Rigidbody _rigidbody;
    public float projectileLifeTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(projectileLifeTime != 0) StartCoroutine(DestroyAfterTime());
        _rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(RotateWithVelocity());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Destroy(transform.root.gameObject);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Structure"))
        {
            Destroy(transform.root.gameObject);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Bulidable"))
        {
            Destroy(transform.root.gameObject);
        }          
    }

    IEnumerator DestroyAfterTime()
    {
        // lifetime 만큼 대기
        yield return new WaitForSeconds(projectileLifeTime);

        // 총알 제거
        Destroy(transform.root.gameObject);
    }

    

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while(true)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidbody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }
}
