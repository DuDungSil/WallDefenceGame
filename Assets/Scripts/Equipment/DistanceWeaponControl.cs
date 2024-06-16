using System.Collections;
using UnityEngine;


public abstract class DistanceWeanponControl : MonoBehaviour
{
    [Header("무기 디폴트 설정")]
    [Space(5)]
    public float damage;
    public float speed;
    public float range;
    public float coolTime;
    public bool useAmmo;
    public int maxAmmo;
    [HideInInspector]
    public int remainAmmo;
    [HideInInspector]
    public float lastShootTime;

    [HideInInspector]
    public bool isCoolTime;
    [HideInInspector]
    public float lastCoolTime;

    protected void Start()
    {
        Init();
    }


    public void Init()
    {

        if(isCoolTime)
        {
            float remainCooltime = Mathf.Max(coolTime - ( Time.time - lastCoolTime ) , 0f); 
            StartCoroutine(ActivateCooldown(remainCooltime));  
        }
        
        OnStatusChanged();
    }

    protected void Reload()
    {
        remainAmmo = maxAmmo;
        SoundController.Instance.PlaySound3D("Gun_reload", gameObject.transform);
        OnStatusChanged();
    } 

    protected IEnumerator ActivateCooldown(float _time)
    {
        lastCoolTime = Time.time;
        yield return new WaitForSeconds(_time);
        isCoolTime = false;
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
