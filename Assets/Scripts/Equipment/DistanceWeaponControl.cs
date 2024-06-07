using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class DistanceWeanponControl : MonoBehaviour
{
    [Header("무기 디폴트 설정")]
    [Space(5)]
    public float damage;
    public float speed;
    public float range;
    public float coolTime;
    public bool useAmmo;
    [ShowIf("useAmmo")]
    public int maxAmmo;
    [HideInInspector]
    public int remainAmmo;
    [HideInInspector]
    public float lastShootTime;

    [HideInInspector]
    public bool isCoolTime = false;
    [HideInInspector]
    public float lastCoolTime;

    void Start()
    {
        if(useAmmo) Reload();
    }

    protected IEnumerator ActivateCooldown(float _time)
    {
        lastCoolTime = Time.time;
        yield return new WaitForSeconds(_time);
        isCoolTime = false;
        if(useAmmo) Reload();
        OnStatusChanged();
    }

    public void LoadData(float _lastCoolTime, int _remainAmmo)
    {
        // 탄창을 사용하냐 안하냐
        if(useAmmo)
        {
            // 이전에 쿨타임이었다면
            if(isCoolTime)
            {
                float remainCooltime = Mathf.Max(coolTime - ( Time.time - _lastCoolTime ) , 0f); 
                StartCoroutine(ActivateCooldown(remainCooltime));
            }
            else
            {
                float timeSinceLastShot = Time.time - lastShootTime;
                // 마지막 슛 시점에서 쿨타임만큼 지났다면 재장전
                if(timeSinceLastShot >= coolTime)
                {
                    Reload();
                }
                // 지나지 않았다면 장탄수 적용
                else
                {
                    remainAmmo = _remainAmmo;
                }
            }

        }
        else
        {
            // 이전에 쿨타임이었다면
            if(isCoolTime)
            {
                float remainCooltime = Mathf.Max(coolTime - ( Time.time - _lastCoolTime ) , 0f); 
                StartCoroutine(ActivateCooldown(remainCooltime));  
            }
        }
        OnStatusChanged();
    }

    protected void Reload()
    {
        remainAmmo = maxAmmo;
        SoundController.Instance.PlaySound3D("Gun_reload", gameObject.transform);
        OnStatusChanged();
    } 

    public delegate void StatusChanged();
    public event StatusChanged onStatusChanged;

    // 무기 상태 변경 이벤트 호출 메서드
    protected void OnStatusChanged()
    {
        if (onStatusChanged != null)
            onStatusChanged.Invoke();
    }

}
