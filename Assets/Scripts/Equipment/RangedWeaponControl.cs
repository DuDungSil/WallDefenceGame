using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class RangedWeaponControl : MonoBehaviour
{
    public InputActionProperty triggerButtonAction;
    public GameObject projectileSpwanPos;
    public GameObject prefab;
    public float shootDelay;
    public float m_speed;

    private bool shootActivate = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 트리거 버튼 누를 시
        if(triggerButtonAction.action.WasPerformedThisFrame() && shootActivate)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        shootActivate = false;
        StartCoroutine(ShootDelay(shootDelay));
        // 투사체 소환
        GameObject spawnedProjectile = Instantiate(prefab, projectileSpwanPos.transform.position, projectileSpwanPos.transform.rotation);
        // 데미지 설정
        spawnedProjectile.GetComponent<ProjectileControl>().damage = 10;
        // 물리속성 설정
        Rigidbody r = spawnedProjectile.GetComponent<Rigidbody>();
        r.AddForce(projectileSpwanPos.transform.forward * m_speed, ForceMode.Impulse);
    }

    IEnumerator ShootDelay(float _shootDelay)
    {
        yield return new WaitForSeconds(_shootDelay);
        shootActivate = true;
    }

    // 원거리 공격

    // 특정 투사체를 날림

    // 투사체가 생성되는 위치를 지정

    // 투사체가 몇개 남았는지 체크
}
