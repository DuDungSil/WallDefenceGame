using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerExplosionControl : ProjectileControl
{
    void Start()
    {
        StartCoroutine(DestroyAfterTime());
        SoundController.Instance.PlaySound3D("Cannonbomb_explosion", gameObject.transform);
    }
    protected override void OnTriggerEnter(Collider other)
    {
        //projectileLifeTime으로 destroy가 제어되도록 함.
    }

}
