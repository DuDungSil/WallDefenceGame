using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StructureManager : MonoBehaviour
{
    private XRSimpleInteractable interactable;
    public GameObject Units;
    protected bool isAssigned = false;
    public bool IsAssigned{
        get{ return isAssigned; }
        protected set{isAssigned = value;}
    }
    public int grade;
    public int neededUnits;
    [SerializeField]
    protected float hp;
    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    public virtual void TakeDamage(float damage)
    {
        Hp = Hp - damage;
        if (Hp < 0)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        if(InteractionManager.Instance != null && interactable != null)
        {
            interactable.selectEntered.AddListener((args) => InteractionManager.Instance.forBuildingInteraction(args));
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
        if(isAssigned)
        {
            isAssigned = false;
            if(Units != null)
            {
                Units.SetActive(false);
            }
        }
        else
        {
            isAssigned = true;
            if(Units != null)
            {
                Units.SetActive(true);
            }
        }
    }
    public void Selfdestroy() // DestroyBtn의 이벤트함수
    {
        Debug.Log("부수기 작동");
    }
    public void SelfUpgrade() //UpgradeBtn의 이벤트함수
    {
        Debug.Log("업그레이드 작동");
    }
}
