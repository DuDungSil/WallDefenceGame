using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class RangedWeaponControl : MonoBehaviour
{
    public float range;
    public float damage;
    public float shootDelay;
    public float m_speed;

    protected bool shootActivate = true;

    protected IEnumerator ShootDelay(float _shootDelay)
    {
        yield return new WaitForSeconds(_shootDelay);
        shootActivate = true;
    }

    // 원거리 공격

    // 특정 투사체를 날림

    // 투사체가 생성되는 위치를 지정

    // 투사체가 몇개 남았는지 체크
}
