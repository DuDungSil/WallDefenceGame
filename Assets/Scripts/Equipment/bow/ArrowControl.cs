using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class ArrowControl : ProjectileControl
{
    public Transform tip;
    public GameObject notchPos;

    [HideInInspector]
    public float maxDamage;
    [HideInInspector]
    public float maxSpeed = 30f;
    [HideInInspector]
    public float maxRange;
    [HideInInspector]
    public float additionalTime;

    private Rigidbody _rigidbody;
    private bool _inAir = false;
    private Vector3 _lastPosition = Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        PullInteraction.PullActionReleased += Release;

        Stop();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release;
    }

    private void Release(float value)
    {
        PullInteraction.PullActionReleased -= Release;
        gameObject.transform.parent = null;
        _inAir = true;
        SetPhysics(true);

        // arrow 날아가는 힘 계산
        Vector3 force = transform.forward * value * maxSpeed;

        // arrow 공격력 계산
        damage = value * maxDamage;

        // arrow lifetime 계산
        float lifetime =  maxRange / maxSpeed * value + additionalTime;


        // arrow 발사시 물리 속성
        _rigidbody.AddForce(force, ForceMode.Impulse);

        setLifeTime(lifetime);
        
        StartCoroutine(RotateWithVelocity());

        _lastPosition = tip.position;
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while(_inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidbody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    void FixedUpdate()
    {
        if(_inAir)
        {
            //CheckCollision();
            _lastPosition = tip.position;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        int hitlayer = other.gameObject.layer;
            // 충돌 레이어 처리
        if(hitlayer == 3 || hitlayer == 6 || hitlayer == 8 || hitlayer == 9 || hitlayer == 10)
        {
            if(other.transform.TryGetComponent(out Rigidbody body))
            {
                _rigidbody.interpolation = RigidbodyInterpolation.None;
                transform.parent = other.transform;
                body.AddForce(_rigidbody.velocity, ForceMode.Impulse);
            }
            Stop();
        }
    }

    private void CheckCollision()
    {
        // if(Physics.Linecast(_lastPosition, tip.position, out RaycastHit hitinfo))
        // {
        //     int hitlayer = hitinfo.transform.gameObject.layer;
        //     // 충돌 레이어 처리
        //     if(hitlayer == 3 || hitlayer == 6 || hitlayer == 8 || hitlayer == 9 || hitlayer == 10)
        //     {
        //         if(hitinfo.transform.TryGetComponent(out Rigidbody body))
        //         {
        //             _rigidbody.interpolation = RigidbodyInterpolation.None;
        //             transform.parent = hitinfo.transform;
        //             body.AddForce(_rigidbody.velocity, ForceMode.Impulse);
        //         }
        //         Stop();
        //         Debug.Log("dd");
        //     }
        // }
    }

    private void Stop()
    {
        _inAir = false;
        SetPhysics(false);
        damage = 0;
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidbody.useGravity = usePhysics;
        _rigidbody.isKinematic = !usePhysics;
    }
}
