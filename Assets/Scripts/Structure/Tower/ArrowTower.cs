using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTower : TowerManager
{
    public override void Shoot(GameObject target)
    {
        // 런칭패드 방향 설정
        Vector3 monsterPos = target.transform.position;
        Vector3 monsterDirection = (monsterPos - shootingPoint.transform.position).normalized;

        Vector3 launchingPadMonsterDirection = monsterDirection;
        launchingPadMonsterDirection.y = 0f;
        
        launchingPad.transform.rotation = Quaternion.identity;

        launchingPad.transform.rotation = Quaternion.LookRotation(launchingPadMonsterDirection);


        // 투사체 소환
        GameObject spawnedProjectile = Instantiate(projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);

        /* 발사체를 일정 속도로 돌리기
        Quaternion rotation = Quaternion.LookRotation(monsterPos);
        launchingPad.transform.rotation = Quaternion.Lerp(launchingPad.transform.rotation, rotation, 10f * Time.deltaTime);
        */

        // 물리속성 설정
        Rigidbody r = spawnedProjectile.GetComponent<Rigidbody>();
        r.AddForce(monsterDirection * m_projectileSpeed, ForceMode.Impulse);
    }
}
