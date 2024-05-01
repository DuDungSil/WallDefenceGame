using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{        
    public XRDirectInteractor interactor;
    public GameObject[] weapons; // 무기 프리팹들

    private GameObject currentWeapon; // 현재 무기
    private bool isGrabbing; // 그랩 상태 확인

    private void Start()
    {
        interactor = GetComponent<XRDirectInteractor>();
        interactor.selectEntered.AddListener(OnGrab); // 그랩 이벤트에 리스너 추가
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbing = true; // 그랩 상태 업데이트
    }

    public void SwitchWeapon(int weaponIndex)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon); // 이전 무기 제거
        }

        // 새로운 무기 생성 및 위치 설정
        currentWeapon = Instantiate(weapons[weaponIndex], transform.position, transform.rotation);

        // 그랩 상태 적용
        if (isGrabbing)
        {
            XRGrabInteractable grabInteractable = currentWeapon.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                // 그랩 상태 직접 설정
                
            }
        }
    }
}
