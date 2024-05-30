using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : RangedWeaponControl
{
    public Transform firePos;
    public GameObject MagicProjectile;

    public float maxChargingTime; 
    [Range (0f, 1f)]
    public float minPower;

    private GameObject _MagicProjectile;
    private float Power;
    private bool isCharging;
    private float ChargingStartTime;

    void Start()
    {
        isCharging = false;
    }

    void Update()
    {
        if(isCharging)
        {   
            // 힘 계산
            Power = Mathf.Min((Time.time - ChargingStartTime) / maxChargingTime , 1f);

            // 투사체 크기 변경
            _MagicProjectile.transform.position = firePos.transform.position;
            _MagicProjectile.transform.localScale = new Vector3(Power, Power, Power);
        }
    }

    public void ChargingStart()
    {
        if(shootActivate == true)
        {
            shootActivate = false;
            StartCoroutine(ShootDelay(shootDelay));

            isCharging = true;

            // 차징 시작 시간 기록
            ChargingStartTime = Time.time;

            Power = minPower;

            // 최소 크기로 소환
            _MagicProjectile = Instantiate(MagicProjectile, firePos.transform.position, Quaternion.identity);
            _MagicProjectile.transform.localScale = new Vector3(Power, Power, Power);
            // 투사체 컨트롤 스크립트
        }
    }

    public void ChargingEnd()
    {
        if(isCharging == true)
        {
            isCharging = false;

            // 데미지 설정
            _MagicProjectile.GetComponent<ProjectileControl>().damage = damage * Power;

            // 투사체 제거 시간 ( 사거리 / 속도 )
            _MagicProjectile.GetComponent<ProjectileControl>().projectileLifeTime = range / m_speed;

            // 물리속성 설정 ( 발사 )
            Rigidbody r = _MagicProjectile.GetComponent<Rigidbody>();
            r.AddForce(firePos.forward * m_speed, ForceMode.Impulse);
        }
    }
}
