using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bow : RangedWeaponControl
{
    public GameObject arrow;
    public GameObject notch;

    private XRGrabInteractable _bow;
    private bool _arrowNotched = false;
    private GameObject _currentArrow = null;


    void Start()
    {
            _bow = GetComponent<XRGrabInteractable>();
            PullInteraction.PullActionReleased += NotchEmpty;    
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= NotchEmpty;
    }

    void Update()
    {
        if(_bow.isSelected && _arrowNotched == false)
        {
            _arrowNotched = true;
            StartCoroutine("DelayedSpawn");
        }
        if(!_bow.isSelected && _currentArrow != null)
        {
            Destroy(_currentArrow);
            NotchEmpty(1f);
        }
    }

    private void NotchEmpty(float value)
    {
        _arrowNotched = false;
        _currentArrow = null;
    }

    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(shootDelay);

        // arrow 생성
        _currentArrow = Instantiate(arrow, notch.transform);

        // arrow 위치 보정
        GameObject arrow_notchPos = arrow.GetComponent<ArrowControl>().notchPos;
        Vector3 pos = Vector3.zero;
        if(arrow_notchPos != null) pos = arrow_notchPos.transform.localPosition;
        _currentArrow.transform.localPosition = _currentArrow.transform.localPosition - pos;

        // arrow 속성 설정
        // 데미지 설정
        _currentArrow.GetComponent<ArrowControl>().maxDamage = damage;

        // 속도 설정
        _currentArrow.GetComponent<ArrowControl>().speed = m_speed;

        // 투사체 제거 시간 ( 사거리 / 속도 )
        _currentArrow.GetComponent<ProjectileControl>().projectileLifeTime = range / m_speed;
    }
}
