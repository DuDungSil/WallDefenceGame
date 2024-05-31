using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Musket : RangedWeaponControl
{
    public Transform firePos;
    public GameObject bullet;

    public int pelletCount = 10; 
    public float spreadAngle = 15f; 
    public float bulletSpeed = 20f; 

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


                for (int i = 0; i < pelletCount; i++)
                {
                    // Calculate a random spread angle within the defined range
                    float spread = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
                    Quaternion rotation = firePos.rotation * Quaternion.Euler(0, spread, 0);

                    // Instantiate the bullet
                    GameObject _bullet = Instantiate(bullet, firePos.position, rotation);

                    // 데미지 설정
                    _bullet.GetComponent<ProjectileControl>().damage = damage;

                    // 투사체 제거 시간 ( 사거리 / 속도 )
                    _bullet.GetComponent<ProjectileControl>().projectileLifeTime = range / m_speed;

                    // 물리속성 설정
                    Rigidbody r = _bullet.GetComponent<Rigidbody>();
                    r.AddForce(firePos.forward * m_speed, ForceMode.Impulse); 

                }

            }

        }
    }
}
