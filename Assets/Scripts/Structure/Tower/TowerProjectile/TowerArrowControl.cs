using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerArrowControl : ProjectileControl
{
    Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(RotateWithVelocity());    
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
