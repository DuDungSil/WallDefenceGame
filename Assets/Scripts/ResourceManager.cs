using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ResourceManager : MonoBehaviour
{
    private XRSimpleInteractable interactable;
    public GameObject Units;
    protected bool isAssigned;
    public bool IsAssigned{
        get{ return isAssigned; }
        protected set{isAssigned = value;}
    }
    [SerializeField]
    protected int neededUnits;
    public int NeededUnits{
        get{ return neededUnits; }
        protected set{neededUnits = value;}
    }
    private void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        if(InteractionManager.Instance != null && interactable != null)
        {
            interactable.selectEntered.AddListener((args) => InteractionManager.Instance.forResourceInteraction(args));
        }
        else
        {
            if(InteractionManager.Instance == null)
            {
                Debug.LogWarning("InteractionUIControl is null");
            }
            if(interactable == null)
            {
                Debug.LogWarning("XRSimpleInteractable can't find");
            }
        }
        
    }
    public void AssignedChange() //AssignBtn과 RemoveBtn의 이벤트함수
    {
        if(isAssigned) //RemoveBtn listener
        {
            UnitController.Instance.assignedUnitNum -= NeededUnits;
            isAssigned = false;
            if(Units != null)
            {
                Units.SetActive(false);
            }
        }
        else //AssignBtn listener
        {
            UnitController.Instance.assignedUnitNum += NeededUnits;
            isAssigned = true;
            if(Units != null)
            {
                Units.SetActive(true);
            }
        }
    }

}
