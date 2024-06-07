using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : DistanceWeanponControl
{
    [Header("피스톨 설정")]
    [Space(5)]
    public Transform firePos;
    public GameObject bullet;
    public float shootDelay;
    // 사격 딜레이
    private bool shootActivate = true;

    void Update()
    {
        float timeSinceLastShot = Time.time - lastShootTime;
        if(timeSinceLastShot > coolTime)
        {
            if(remainAmmo < maxAmmo)
            {
                Reload();
                OnStatusChanged();
            }

        }
    }

    public void Shoot()
    {
        if(isCoolTime == false)
        {

            if(shootActivate == true)
            {

                // 투사체 소환
                GameObject spawnedProjectile = Instantiate(bullet, firePos.position, firePos.rotation);

                // 데미지 설정
                spawnedProjectile.GetComponent<ProjectileControl>().damage = damage;

                // 투사체 제거 시간 ( 사거리 / 속도 )
                spawnedProjectile.GetComponent<ProjectileControl>().projectileLifeTime = range / speed;

                // 물리속성 설정
                Rigidbody r = spawnedProjectile.GetComponent<Rigidbody>();
                r.AddForce(firePos.forward * speed, ForceMode.Impulse);   

                SoundController.Instance.PlaySound3D("Pistol_shoot", gameObject.transform);

                // 마지막 슈팅 시간
                lastShootTime = Time.time;
                // 총알 감소
                remainAmmo--;
                // 총알 다 쓰면 쿨타임 적용
                if(remainAmmo <= 0)
                {
                    isCoolTime = true;
                    StartCoroutine(ActivateCooldown(coolTime));
                } 
                else
                {
                    shootActivate = false;
                    StartCoroutine(ShootDelay());
                }

                OnStatusChanged();
            }

        }
        else
        {
            SoundController.Instance.PlaySound3D("Gun_Ammo_not_exixt", gameObject.transform);
        }
    }

    protected IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        shootActivate = true;
    }
}
