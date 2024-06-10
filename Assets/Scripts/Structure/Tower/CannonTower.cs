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
        // 투사체 방향 설정
        Vector3 monsterPos = target.transform.position;
        Vector3 direction = (monsterPos - shootingPoint.transform.position).normalized;

        // 시작점과 목표점 사이의 거리와 높이 계산
        float distance = Vector3.Distance(new Vector3(monsterPos.x, shootingPoint.transform.position.y, monsterPos.z), shootingPoint.transform.position);
        float heightDifference = monsterPos.y - shootingPoint.transform.position.y;

        // 중력 가속도
        gravity = Physics.gravity.y;

        // 수평 거리로부터 초기 속도 계산 (kinematic equation)
        float initialSpeed = Mathf.Sqrt((distance * Mathf.Abs(gravity)) / Mathf.Sin(2 * launchAngle * Mathf.Deg2Rad));

        // 초기 속도 벡터 계산
        Vector3 velocity = new Vector3(direction.x, Mathf.Tan(launchAngle * Mathf.Deg2Rad), direction.z).normalized * initialSpeed;

        // 투사체 소환
        GameObject spawnedProjectile = Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);

        // 발사체 방향 설정
        spawnedProjectile.transform.rotation = Quaternion.LookRotation(direction);

        // 물리 속성 설정
        Rigidbody r = spawnedProjectile.GetComponent<Rigidbody>();
        r.velocity = velocity;

        // 초기 속도에 높이 차이를 반영하여 수정
        r.velocity = new Vector3(r.velocity.x, CalculateVerticalVelocity(initialSpeed, heightDifference, distance), r.velocity.z);
    }

    private float CalculateVerticalVelocity(float initialSpeed, float heightDifference, float distance)
    {
        // 포물선 운동의 수직 속도 계산
        float verticalVelocity = (heightDifference + 0.5f * Mathf.Abs(Physics.gravity.y) * (distance / initialSpeed) * (distance / initialSpeed)) / (distance / initialSpeed);
        return verticalVelocity;
    }
}
