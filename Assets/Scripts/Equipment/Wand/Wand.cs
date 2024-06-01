using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Wand : RangedWeaponControl
{
    [Header("완드 설정")]
    [Space(5)]
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
            // 선택 중인 인터렉터들의 목록
            var selectingInteractors = GetComponent<XRGrabInteractable>().interactorsSelecting;

            // 선택 중인 인터렉터의 수
            int selectCount = selectingInteractors.Count;
            
            if(selectCount == 0)
            {
                if(_MagicProjectile != null) Destroy(_MagicProjectile);
                isCharging = false;
                isCoolTime = true;
                StartCoroutine(ActivateCooldown(coolTime));
            }
            else
            {
                // 힘 계산
                Power = Mathf.Min((Time.time - ChargingStartTime) / maxChargingTime , 1f);

                // 투사체 크기 변경
                _MagicProjectile.transform.position = firePos.transform.position;
                _MagicProjectile.transform.localScale = new Vector3(Power, Power, Power);
            }

        }

    }

    public void ChargingStart()
    {
        if(isCoolTime == false)
        {
            isCoolTime = true;

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
            _MagicProjectile.GetComponent<ProjectileControl>().setLifeTime(range / speed);

            // 물리속성 설정 ( 발사 )
            Rigidbody r = _MagicProjectile.GetComponent<Rigidbody>();
            r.AddForce(firePos.forward * speed, ForceMode.Impulse);

            StartCoroutine(ActivateCooldown(coolTime));
        }
    }
}
