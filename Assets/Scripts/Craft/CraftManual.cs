using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string craftName; // 이름
    public GameObject go_prefab; // 실제 설치 될 프리팹
    public GameObject go_PreviewPrefab; // 미리 보기 프리팹
}

public class CraftManual : MonoBehaviour
{
    public InputActionProperty TriggerButtonAction; // 트리거 버튼 액션
    public InputActionProperty GribButtonAction; // 그랩 버튼 액션

    private bool isPreviewActivated = false; // 미리 보기 활성화 상태

    [SerializeField]
    private Craft[] craft_tower;  // 타워 탭에 있는 슬롯들. 

    private GameObject go_Preview; // 미리 보기 프리팹을 담을 변수
    private GameObject go_Prefab; // 실제 생성될 프리팹을 담을 변수 

    [SerializeField]
    XRRayInteractor m_RayInteractor;
    public XRRayInteractor rayInteractor => m_RayInteractor;
 
    Vector3 reticlePosition;

    public void SlotClick(int _slotNumber)
    {
        rayInteractor.TryGetHitInfo(out reticlePosition, out _, out _, out _);
        go_Preview = Instantiate(craft_tower[_slotNumber].go_PreviewPrefab, reticlePosition, Quaternion.identity);
        go_Prefab = craft_tower[_slotNumber].go_prefab;

        UIController.Instance.CloseGameMenu();
        isPreviewActivated = true;
    }


    void Update()
    {

        if (isPreviewActivated)
            PreviewPositionUpdate();

        if (isPreviewActivated && TriggerButtonAction.action.WasPerformedThisFrame())
            Build();
        
        if (isPreviewActivated && GribButtonAction.action.WasPerformedThisFrame())
            Cancel();

        if (isPreviewActivated && Input.GetKeyDown(KeyCode.Q))
            go_Preview.transform.Rotate(0f, -30f, 0f);
        else if (isPreviewActivated && Input.GetKeyDown(KeyCode.E))
            go_Preview.transform.Rotate(0f, +30f, 0f);

    }

    private void PreviewPositionUpdate()
    {
        rayInteractor.TryGetHitInfo(out reticlePosition, out _, out _, out _);
        if (reticlePosition != Vector3.zero)
        {
            Vector3 _location = reticlePosition;
            go_Preview.transform.position = _location;
        }
        
    }

    private void Build()
    {
        if(isPreviewActivated && go_Preview.GetComponent<PreviewObject>().isBuildable())
        {
            rayInteractor.TryGetHitInfo(out reticlePosition, out _, out _, out _);
            Instantiate(go_Prefab, reticlePosition, go_Preview.transform.rotation);
            Destroy(go_Preview);
            UIController.Instance.OpenCrafting();
            isPreviewActivated = false;
            go_Preview = null;
            go_Prefab = null;
        }
    }

    private void Cancel()
    {
        if (isPreviewActivated)
            Destroy(go_Preview);

        UIController.Instance.OpenCrafting();
        isPreviewActivated = false;

        go_Preview = null;
        go_Prefab = null;

    }
}
