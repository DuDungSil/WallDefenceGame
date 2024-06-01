using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Musket : RangedWeaponControl
{
    [Header("머스킷 설정")]
    [Space(5)]
    public Transform firePos;
    public GameObject bullet;
    public float shootDelay;
    [Tooltip("샷건 탄환 총알 수")]
    public int pelletCount = 10; 
    public float spreadAngle = 15f; 

    // 사격 딜레이
    private bool shootActivate = true;

    void Update()
    {
        float timeSinceLastShot = Time.time - lastShootTime;
        if(timeSinceLastShot > coolTime)
        {
            remainAmmo = maxAmmo;
        }
    }

    public void Shoot()
    {
        // 딜레이 끝나면
        if(isCoolTime == false)
        {
            // 선택 중인 인터렉터들의 목록
            var selectingInteractors = GetComponent<XRGrabInteractable>().interactorsSelecting;

            // 선택 중인 인터렉터의 수
            int selectCount = selectingInteractors.Count;

            if(selectCount >= 2)
            {

                if(shootActivate == true)
                {
                    shootActivate = false;

                    for (int i = 0; i < pelletCount; i++)
                    {
                        // Calculate a random spread angle within the defined range
                        float spread = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
                        Quaternion rotation = firePos.rotation * Quaternion.Euler(0, spread, 0);

                        // Instantiate the bullet
                        GameObject _bullet = Instantiate(bullet, firePos.position, rotation);

                        // 데미지 설정
                        _bullet.GetComponent<ProjectileControl>().damage = damage / pelletCount;

                        // 투사체 제거 시간 ( 사거리 / 속도 )
                        _bullet.GetComponent<ProjectileControl>().projectileLifeTime = range / speed;

                        // 물리속성 설정
                        Rigidbody r = _bullet.GetComponent<Rigidbody>();
                        r.AddForce(firePos.forward * speed, ForceMode.Impulse); 

                    }

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
                        StartCoroutine(ShootDelay());
                    }

                }
                
            }

        }
    }

    protected IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        shootActivate = true;
    }
}
