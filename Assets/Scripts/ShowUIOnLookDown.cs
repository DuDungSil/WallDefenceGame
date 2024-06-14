using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ShowUIOnLookDown : MonoBehaviour
{
    public GameObject uiElement; // 나타낼 UI 오브젝트
    public float lookDownThreshold = 45.0f; // 고개를 숙이는 각도 임계값

    void Update()
    {
        // HMD의 로테이션 정보를 가져오기
        Quaternion headRotation;
        if (XRDevice.GetTrackingSpaceType() == TrackingSpaceType.RoomScale)
        {
            headRotation = InputTracking.GetLocalRotation(XRNode.Head);
        }
        else
        {
            headRotation = Camera.main.transform.rotation;
        }

        // pitch 각도 계산
        float pitch = headRotation.eulerAngles.x;
        if (pitch > 180) pitch -= 360; // 각도를 -180 ~ 180 범위로 조정

        // 고개를 숙였는지 확인
        if (pitch > lookDownThreshold)
        {
            // UI 활성화
            uiElement.SetActive(true);
        }
        else
        {
            // UI 비활성화
            uiElement.SetActive(false);
        }
    }
}
