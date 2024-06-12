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
    [SerializeField]
    protected int neededUnits;
    public int NeededUnits{
        get{ return neededUnits;} 
        protected set{ neededUnits = value;}
    }
    [SerializeField]
    protected float maxHp;
    public float MaxHp
    {
        get { return maxHp; }
        protected set { maxHp = value; }
    }
    protected float hp;
    public float Hp
    {
        get { return hp; }
        protected set { hp = Mathf.Min(value,MaxHp); }
    }

    [SerializeField]
    public BuildingBOM nextUpgrade;

    public virtual void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("MonsterWeapon"))
        {
            MonsterWeaponDamage monsterWeaponDamage = other.GetComponent<MonsterWeaponDamage>();
            TakeDamage(monsterWeaponDamage.m_damage);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("MonsterProjectile"))
        {
            MonsterWeaponDamage monsterWeaponDamage = other.gameObject.transform.root.GetComponent<MonsterWeaponDamage>();
            TakeDamage(monsterWeaponDamage.m_damage);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("PlayerTool"))
        {
            if(other.gameObject.tag == "RepairTool")
            {
                ToolControl toolControl = other.gameObject.GetComponentInParent<ToolControl>();
                Repair(MaxHp * toolControl.value);
            } 
        }
    }

    public virtual void TakeDamage(float damage)
    {
        Hp = Hp - damage;
        //Debug.Log(Hp);
        if (Hp < 0)
        {
            Debug.Log(Hp);
            SoundController.Instance.PlaySound2D("Building_destroy");
            Destroy(gameObject);
        }
    }
    public void Awake()
    {
        Hp = MaxHp;
        if (nextUpgrade.Data != null)
        {
            nextUpgrade.Init();
        }
    }
    protected virtual void Start()
    {
        //Hp = MaxHp;
        //if(nextUpgrade.Data != null)
        //{
        //    nextUpgrade.Init();
        //}

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
        if(isAssigned) //RemoveBtn listener
        {
            if(UnitController.Instance.RemoveUnits(NeededUnits))
            {
                isAssigned = false;
                if(Units != null)
                {
                    Units.SetActive(false);
                }
            }
        }
        else //AssignBtn listener
        {
            if(UnitController.Instance.AssignUnits(NeededUnits))
            {
                isAssigned = true;
                if(Units != null)
                {
                    Units.SetActive(true);
                }
            }
        }
    }
    public void Selfdestroy() // DestroyBtn의 이벤트함수
    {
        Debug.Log("부수기 작동");
        // 할당된 unit을 UnitController에 돌려주는 코드
        if(isAssigned)
            UnitController.Instance.RemoveUnits(NeededUnits);

        SoundController.Instance.PlaySound2D("Building_destroy");
        Destroy(gameObject);
    }
    public GameObject SelfUpgrade() //UpgradeBtn의 이벤트함수
    {
        Debug.Log("업그레이드 작동");
        GameObject upgradeObj = Instantiate(nextUpgrade.Data.prefab, gameObject.transform.position, gameObject.transform.rotation);
        ResourceDatabase.Instance.DecreaseResource(nextUpgrade.craftNeedItems, nextUpgrade.Data.craftNeedItemCount);
        // 할당된 unit을 UnitController에 돌려주는 코드
        if(isAssigned)
            UnitController.Instance.RemoveUnits(NeededUnits);

        
        return upgradeObj;
    }
    
    public void Repair(float recovery)
    {
        Hp = Hp + recovery;
    }

}
