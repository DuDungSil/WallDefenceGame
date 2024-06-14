using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StructureManager : MonoBehaviour
{
    private XRSimpleInteractable interactable;
    public string structureName;
    public int grade;

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

    protected virtual void Start()
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
                if(toolControl.Repair()) Repair(MaxHp * toolControl.value);
            } 
        }
    }

    public virtual void TakeDamage(float damage)
    {
        Hp = Hp - damage;
        Debug.Log(Hp);
        if (Hp <= 0)
        {
            Debug.Log(Hp);
            SoundController.Instance.PlaySound2D("Building_destroy");
            Destroy(gameObject);
        }
        OnStatusChanged();
    }
    
    public void Repair(float recovery)
    {
        if(Hp + recovery > MaxHp)
            Hp = MaxHp;
        else
            Hp = Hp + recovery;
        OnStatusChanged();
    }

    public void Awake()
    {
        Hp = MaxHp;
        if (nextUpgrade.Data != null)
        {
            nextUpgrade.Init();
        }
    }
    public virtual void Selfdestroy() // DestroyBtn의 이벤트함수
    {
        Debug.Log("부수기 작동");
        // 할당된 unit을 UnitController에 돌려주는 코드

        SoundController.Instance.PlaySound2D("Building_destroy");
        Destroy(gameObject);
    }
    public virtual GameObject SelfUpgrade() //UpgradeBtn의 이벤트함수
    {
        Debug.Log("업그레이드 작동");
        GameObject upgradeObj = Instantiate(nextUpgrade.Data.prefab, gameObject.transform.position, gameObject.transform.rotation);
        ResourceDatabase.Instance.DecreaseResource(nextUpgrade.craftNeedItems, nextUpgrade.Data.craftNeedItemCount);

        
        return upgradeObj;
    }


    public delegate void StatusChanged();
    public event StatusChanged onStatusChanged;

    // 스트럭쳐 hp 변경 이벤트 호출 메서드
    protected void OnStatusChanged()
    {
        if (onStatusChanged != null)
            onStatusChanged.Invoke();
    }

}
