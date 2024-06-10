using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerExplosionControl : ProjectileControl
{
    protected override void OnTriggerEnter(Collider other)
    {
        //projectileLifeTime으로 destroy가 제어되도록 함.
    }

}
