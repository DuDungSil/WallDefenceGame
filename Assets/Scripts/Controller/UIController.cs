using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : Singleton<UIController>
{
    public InputActionProperty menuButtonAction;
    public InputActionProperty Ybutton;
    public InputActionProperty Xbutton;
    public InputActionProperty Bbutton;
    public InputActionProperty Abutton;
    
    public GameObject m_pauseCanvas;
    public GameObject m_CraftingCanvas;
    public GameObject m_SettingCanvas;
    public GameObject m_InteractionMenuCanvas;
    public GameObject m_swapper;
    public GameObject m_GameoverCanvas;
    public GameObject m_RightHand;
    public Transform m_mainCamera;
    public Transform m_UIPos;

    private GameObject m_Quick;
    private bool isOpenMenu = false;
    private bool isOpenCrafting = false;
    private bool isOnCrafting = false;
    private bool isOpenSetting = false;
    private bool isOpenQuick = false;
    private bool isOpenInteraction = false;
    private bool isCollisionToInteractionObject = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(menuButtonAction.action.WasPerformedThisFrame() && !isOpenCrafting&& !isOpenQuick&& !isOpenSetting && !isOpenInteraction)
        {
            if(isOpenMenu == false)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }

        if(Ybutton.action.WasPerformedThisFrame() && !isOpenMenu && !isOpenQuick && !isOpenSetting && !isOpenInteraction)
        {       
            if(isOpenCrafting == false)
            {
                OpenCrafting();
            }
            else
            {
                if(isOnCrafting == false)
                {
                    CloseCrafting();
                }
            }
        }

        if(Xbutton.action.WasPerformedThisFrame() && !isOpenMenu && !isOpenCrafting && !isOpenQuick && !isOpenInteraction)
        {
            if(isOpenSetting == false)
            {
                OpenSetting();
            }
            else
            {
                CloseSetting();
            }
        }

        if(Bbutton.action.WasPerformedThisFrame() && !isOpenMenu && !isOpenCrafting && !isOpenSetting && !isOpenInteraction)
        {
            if(isOpenQuick == false)
            {
                OpenQuick();
            }
            else
            {
                CloseQuick();
            }
        }
        if(Abutton.action.WasPerformedThisFrame() && !isOpenMenu && !isOpenCrafting && !isOpenSetting && !isOpenQuick)
        {
            if(isOpenInteraction == false)
            {
                OpenInteraction();
            }
            else
            {
                CloseInteraction();
            }
        }
        
        



    }

    public void OpenMenu()
    {
        HandController.Instance.DeleteEquipObject();
        HandController.Instance.SetUIController();
        DisplayUI(m_pauseCanvas);
        isOpenMenu = true;
    }

    public void CloseMenu()
    {
        HandController.Instance.SetGrabController();
        m_pauseCanvas.SetActive(false);
        isOpenMenu = false;       
    }

    public void OpenCrafting()
    {
        HandController.Instance.DeleteEquipObject();
        HandController.Instance.SetUIController();
        DisplayUI(m_CraftingCanvas);
        isOpenCrafting = true;
    }

    public void CloseCrafting()
    {
        HandController.Instance.SetGrabController();
        m_CraftingCanvas.SetActive(false);
        isOpenCrafting = false;
    }

    // 크래프팅 중
    public void OnCrafting()
    {
        m_CraftingCanvas.SetActive(false);
        isOnCrafting = true;
    }

    // 크래프팅 종료
    public void OffCrafting()
    {
        DisplayUI(m_CraftingCanvas);
        isOnCrafting = false;
    }

    public void OpenSetting()
    {
        HandController.Instance.DeleteEquipObject();
        HandController.Instance.SetUIController();
        DisplayUI(m_SettingCanvas);
        isOpenSetting = true;
    }

    public void CloseSetting()
    {
        HandController.Instance.SetGrabController();
        m_SettingCanvas.SetActive(false);
        isOpenSetting = false;
    }

    public void OpenQuick()
    {   
        HandController.Instance.DeleteEquipObject();
        m_Quick = Instantiate(m_swapper, m_RightHand.transform.position, m_RightHand.transform.rotation);
        isOpenQuick = true;
    }

    public void CloseQuick()
    {
        Destroy(m_Quick);
        isOpenQuick = false;
    } 

    public void OpenInteraction()
    {
        HandController.Instance.DeleteEquipObject();
        HandController.Instance.SetUIController();
        m_InteractionMenuCanvas.SetActive(true);
        //m_InteractionMenuCanvas.transform.position = m_UIPos.position;
        isOpenInteraction = true;
    }

    public void CloseInteraction()
    {
        HandController.Instance.SetGrabController();
        m_InteractionMenuCanvas.SetActive(false);
        isOpenInteraction = false;
    }
    public void OpenGameover()
    {
        HandController.Instance.DeleteEquipObject();
        HandController.Instance.SetUIController();
        DisplayUI(m_GameoverCanvas);
    }  

    public void DisplayUI(GameObject canvas)
    {
        // 회전량
        Vector3 direction = m_mainCamera.position - m_UIPos.position; // 플레이어와 UI의 방향 벡터 계산
        direction.y = 0; // Y축 회전을 고정
        direction = -direction;
        Quaternion rotation = Quaternion.LookRotation(direction); // 방향 벡터를 회전으로 변환

        // y축 이동
        RectTransform rectTransform = canvas.GetComponent<RectTransform>();
        Vector3 pos = m_UIPos.position;
        float newy = Mathf.Max(m_UIPos.position.y, m_mainCamera.position.y + rectTransform.rect.height * rectTransform.localScale.y / 2 );
        pos.y = newy;

        canvas.SetActive(true);
        canvas.transform.rotation = rotation;
        canvas.transform.position = pos;
    }
}
