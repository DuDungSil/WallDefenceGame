using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : Singleton<UIController>
{
    public InputActionProperty menuButtonAction;
    public InputActionProperty primaryButtonAction;
    public InputActionProperty secondaryButtonAction;
    public GameObject m_pauseCanvas;
    public GameObject m_CraftingCanvas;
    public GameObject m_swapper;
    public GameObject m_RightHand;
    public Transform m_UIPos;
    GameObject m_Quick;

    private bool isOpenMenu = false;
    private bool isOpenCrafting = false;
    private bool isOnCrafting = false;
    private bool isOpenQuick = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(menuButtonAction.action.WasPerformedThisFrame() && isOpenCrafting != true && isOpenQuick != true)
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

        if(primaryButtonAction.action.WasPerformedThisFrame() && isOpenMenu != true && isOpenQuick != true)
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

        if(secondaryButtonAction.action.WasPerformedThisFrame() && isOpenMenu != true && isOpenCrafting != true)
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


    }

    public void OpenMenu()
    {
        HandController.Instance.SetUIController();
        m_pauseCanvas.SetActive(true);
        m_pauseCanvas.transform.position = m_UIPos.position;
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
        HandController.Instance.SetUIController();
        m_CraftingCanvas.SetActive(true);
        m_CraftingCanvas.transform.position = m_UIPos.position;
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
        m_CraftingCanvas.transform.position = m_UIPos.position;
        m_CraftingCanvas.SetActive(true);
        isOnCrafting = false;
    }

    public void OpenQuick()
    {   
        HandController.Instance.SetBareHands();
        m_Quick = Instantiate(m_swapper, m_RightHand.transform.position, m_RightHand.transform.rotation);
        isOpenQuick = true;
    }

    public void CloseQuick()
    {
        Destroy(m_Quick);
        isOpenQuick = false;
    } 
}
