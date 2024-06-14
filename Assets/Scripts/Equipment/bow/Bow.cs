using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Bow : DistanceWeanponControl
{
    [Header("활 설정")]
    [Space(5)]
    public GameObject arrow;
    public GameObject notch;
    public float arrowSpawnDelay;
    [Tooltip("화살이 박힌 후 남아있을 시간")]
    public float additionalTime = 3f;

    private XRGrabInteractable _bow;
    private bool _arrowNotched = false;
    private GameObject _currentArrow = null;


    new void Start()
    {
        base.Start();
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
        yield return new WaitForSeconds(arrowSpawnDelay);

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
        _currentArrow.GetComponent<ArrowControl>().maxSpeed = speed;

        // 최대 사거리
        _currentArrow.GetComponent<ArrowControl>().maxRange = range;

        // 투사체 제거 시간 ( 사거리 / 속도 )
        _currentArrow.GetComponent<ArrowControl>().additionalTime = additionalTime;

        SoundController.Instance.PlaySound3D("Arrow_spawn", gameObject.transform);
    }
}
