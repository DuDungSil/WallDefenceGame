using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCannonBombControl : ProjectileControl
{
    public GameObject bombEffect;
    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            GameObject explosion = Instantiate(bombEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Bulidable"))
        {
            GameObject explosion = Instantiate(bombEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
