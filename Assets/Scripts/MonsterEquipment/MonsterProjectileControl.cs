using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectileControl : MonsterWeaponDamage
{
    Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(RotateWithVelocity());
    }

    // Update is called once per frame
    void Update()
    {
        
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
