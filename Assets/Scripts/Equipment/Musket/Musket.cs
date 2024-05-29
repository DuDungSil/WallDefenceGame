using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Musket : RangedWeaponControl
{
    public Transform firePos;
    public GameObject bullet;

    public void Shoot()
    {
        // 딜레이 끝나면
        if(shootActivate == true)
        {
            // 선택 중인 인터렉터들의 목록
            var selectingInteractors = GetComponent<XRGrabInteractable>().interactorsSelecting;

            // 선택 중인 인터렉터의 수
            int selectCount = selectingInteractors.Count;

            if(selectCount >= 2)
            {
                shootActivate = false;
                StartCoroutine(ShootDelay(shootDelay));

                // 투사체 소환
                GameObject spawnedProjectile = Instantiate(bullet, firePos.position, firePos.rotation);

                // 데미지 설정
                spawnedProjectile.GetComponent<ProjectileControl>().damage = damage;

                // 투사체 제거 시간 ( 사거리 / 속도 )
                spawnedProjectile.GetComponent<ProjectileControl>().projectileLifeTime = range / m_speed;

                // 물리속성 설정
                Rigidbody r = spawnedProjectile.GetComponent<Rigidbody>();
                r.AddForce(firePos.forward * m_speed, ForceMode.Impulse); 
            }

        }
    }
}
