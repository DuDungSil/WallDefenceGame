using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : TowerManager
{
    public float launchAngle;
    protected float distance;
    protected float gravity;
    protected float projectileSpeed;
    public override void Shoot(GameObject target)
    {
        //투사체 방향 설정
        Vector3 monsterPos = target.transform.position;
        Vector3 monsterDirection = (monsterPos - shootingPoint.transform.position).normalized;
        launchingPad.transform.rotation = Quaternion.LookRotation(monsterDirection); // 발사대 방향 설정
        distance = Vector3.Distance(monsterPos, shootingPoint.transform.position);
        gravity = Physics.gravity.y;
        // 수평 거리로부터 초기 속도 계산
        projectileSpeed = Mathf.Sqrt(distance * Mathf.Abs(gravity) / Mathf.Sin(2 * launchAngle * Mathf.Deg2Rad));

        // 발사 각도로부터 초기 속도 벡터 계산
        Vector3 velocity = new Vector3(monsterDirection.x, distance * Mathf.Tan(launchAngle * Mathf.Deg2Rad), monsterDirection.z).normalized * projectileSpeed;

        // 투사체 소환
        GameObject spawnedProjectile = Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);

        // 발사체 방향 설정
        spawnedProjectile.transform.rotation = Quaternion.LookRotation(monsterDirection);

        // 물리 속성 설정
        Rigidbody r = spawnedProjectile.GetComponent<Rigidbody>();
        r.velocity = velocity;
    }
}
