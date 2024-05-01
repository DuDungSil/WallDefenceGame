using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : Singleton<UIController>
{
    public InputActionProperty menuButtonAction;
    public InputActionProperty secondaryButtonAction;
    public GameObject m_pauseCanvas;
    public GameObject m_GameMenuCanvas;
    public GameObject m_inventoryBase;
    public GameObject m_craftingBase;
    public Transform m_UIPos;

    private bool isOpenGameMenu = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(menuButtonAction.action.WasPerformedThisFrame())
        {
            Debug.Log("M");
            m_pauseCanvas.SetActive(!m_pauseCanvas.activeSelf);
            m_pauseCanvas.transform.position = m_UIPos.position;
        }

        if(secondaryButtonAction.action.WasPerformedThisFrame())
        {
            if(isOpenGameMenu == false)
            {
                OpenIventory();
            }
            else
            {
                CloseGameMenu();
            }
        }
    }

    public void ChangeGameMenu(int i)
    {
        if(i == 0)
        {
            m_inventoryBase.SetActive(true);
            m_craftingBase.SetActive(false);
        }
        else if(i == 1)
        {
            m_inventoryBase.SetActive(false);
            m_craftingBase.SetActive(true);
        }
    }

    public void OpenIventory()
    {
        HandController.Instance.SetUIController();
        m_GameMenuCanvas.SetActive(true);
        ChangeGameMenu(0);
        m_GameMenuCanvas.transform.position = m_UIPos.position;
        isOpenGameMenu = true;
    }

    public void OpenCrafting()
    {
        HandController.Instance.SetUIController();
        m_GameMenuCanvas.SetActive(true);
        ChangeGameMenu(1);
        m_GameMenuCanvas.transform.position = m_UIPos.position;
        isOpenGameMenu = true;
    }

    public void CloseGameMenu()
    {
        HandController.Instance.SetGrabController();
        m_GameMenuCanvas.SetActive(false);
        isOpenGameMenu = false;
    }

}
