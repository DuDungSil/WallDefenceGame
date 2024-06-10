using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : TowerManager
{
    public GameObject cannon;
    public float launchAngle;
    protected float distance;
    protected float gravity;
    protected float projectileSpeed;

    public override void Shoot(GameObject target)
    {
        // // 투사체 방향 설정
        Vector3 monsterPos = target.transform.position;
        Vector3 monsterDirection = (monsterPos - shootingPoint.transform.position).normalized;
        monsterDirection.y = 0;

        launchingPad.transform.rotation = Quaternion.identity;

        cannon.transform.rotation = Quaternion.Euler(-launchAngle, 0, 0);

        launchingPad.transform.rotation = Quaternion.LookRotation(monsterDirection);


        // // 투사체 소환
        GameObject spawnedProjectile = Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);
        Rigidbody rb = spawnedProjectile.GetComponent<Rigidbody>();

        rb.velocity = GetVelocity(shootingPoint.transform.position, monsterPos, 0f);
    }

    public Vector3 GetVelocity(Vector3 player, Vector3 target, float initialAngle)
    {
        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(target.x, 0, target.z);
        Vector3 planarPosition = new Vector3(player.x, 0, player.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = player.y - target.y;

        float initialVelocity
            = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity
            = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects
            = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (target.x > player.x ? 1 : -1);
        Vector3 finalVelocity
            = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        return finalVelocity;
    }
}
