using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine;

public class Craft : Singleton<Craft>
{
    // 인풋 시스템
    public InputActionProperty TriggerButtonAction; // 트리거 버튼 액션
    public InputActionProperty BbuttonAction; // b 버튼 액션
    public InputActionProperty RightGribButtonAction; // 오른쪽 그랩 버튼 액션
    public InputActionProperty LeftGribButtonAction; // 왼쪽 그랩 버튼 액션
    [SerializeField]
    XRRayInteractor m_RayInteractor;
    public XRRayInteractor rayInteractor => m_RayInteractor;

    // 크래프팅 세팅 변수
    private GameObject craft_Preview;
    private GameObject craft_Prefab;
    private ResourceItem[] needitem;
    private int[] needitem_count;

    // 제어 변수
    private bool isPreviewActivated = false; // 미리 보기 활성화 상태
    private GameObject go_Preview; // 미리 보기 프리팹을 담을 변수
    private Vector3 reticlePosition; // 레이 위치
    private Vector3 InitialRayPosition; // 레이 초기 위치
    private bool isInitialRayPosition = false;


    public void CraftButtonClick()
    {
        UIController.Instance.OnCrafting();
        
        // ui가 꺼진 후 손을 움직이지 않으면 레이 위치가 그대로
        rayInteractor.TryGetHitInfo(out reticlePosition, out _, out _, out _);
        InitialRayPosition = reticlePosition;
        isInitialRayPosition = true;
        go_Preview = Instantiate(craft_Preview, reticlePosition, Quaternion.identity);
        go_Preview.SetActive(false);
        
        
        isPreviewActivated = true;
    }

    // 크레프트 세팅 함수 ( 미리보기 프리팹, 실제 생성 프리팹, 필요 아이템 배열, 필요 아이템 카운트 배열)
    public void SetCraft(GameObject _craft_Prefab, GameObject _craft_Preview, ResourceItem[] _needitem, int[] _needitem_count)
    {
        craft_Prefab = _craft_Prefab;
        craft_Preview = _craft_Preview;
        needitem = _needitem;
        needitem_count = _needitem_count;
    }

    void Update()
    {

        if (isPreviewActivated)
            PreviewPositionUpdate();

        if (isPreviewActivated && TriggerButtonAction.action.WasPerformedThisFrame())
            Build();
        
        if (isPreviewActivated && BbuttonAction.action.WasPerformedThisFrame())
            Cancel();

        if (isPreviewActivated && LeftGribButtonAction.action.WasPerformedThisFrame())
            go_Preview.transform.Rotate(0f, +30f, 0f);
        else if (isPreviewActivated && RightGribButtonAction.action.WasPerformedThisFrame())
            go_Preview.transform.Rotate(0f, -30f, 0f);

    }

    private void PreviewPositionUpdate()
    {
        rayInteractor.TryGetHitInfo(out reticlePosition, out _, out _, out _);
        if (reticlePosition != InitialRayPosition) isInitialRayPosition = false;
        if (reticlePosition != Vector3.zero && !isInitialRayPosition)
        {
            Vector3 _location = reticlePosition;
            go_Preview.transform.position = _location;
            go_Preview.SetActive(true);
        }
        else go_Preview.SetActive(false);
        
        
    }

    private void Build()
    {
        if(reticlePosition == Vector3.zero)
        {
            Cancel();
        }

        if(isPreviewActivated && go_Preview.GetComponent<PreviewObject>().isBuildable())
        {
            rayInteractor.TryGetHitInfo(out reticlePosition, out _, out _, out _);
            Instantiate(craft_Prefab, reticlePosition, go_Preview.transform.rotation);
            Destroy(go_Preview);
            UIController.Instance.OffCrafting();
            ResourceDatabase.Instance.DecreaseResource(needitem, needitem_count);

            SoundController.Instance.PlaySound2D("Craft_building");

            isPreviewActivated = false;
            go_Preview = null;
            craft_Prefab = null;
        }
    }

    private void Cancel()
    {
        if (isPreviewActivated)
            Destroy(go_Preview);

        UIController.Instance.OffCrafting();
        isPreviewActivated = false;

        go_Preview = null;
        craft_Preview = null;
        craft_Prefab = null;

    }
}
