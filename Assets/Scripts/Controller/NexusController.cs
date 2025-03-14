using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusController : Singleton<NexusController>
{
    public GameObject[] attackPoint;
    [SerializeField]
    private float maxHp;
    private float hp;
    [SerializeField]
    private float recoveryAmount;
    [SerializeField]
    private float recoveryTime;  
    private Vector3 currentScale;
    public GameObject hpBar;
    public Material redMaterial;
    public Material greenMaterial;
    public Renderer currentHpRenderer;

    // 넥서스 공격받는 메세지
    public GameObject messagePanel;

    // 제어변수
    private bool isRecoveryCooltime = false;
    private bool isMessageCooltime = false;

    public float Hp
    {
        get { return hp; }
        private set { hp = Mathf.Min(value, maxHp); }
    }
    void Start()
    {
        messagePanel.SetActive(false);
        Hp = maxHp;
    }
    void Update()
    {
        // 자연치유 코드
        if(hp < maxHp)
        {
            if(!isRecoveryCooltime)
            {
                Recovery(recoveryAmount);
                isRecoveryCooltime = true;
                StartCoroutine(ActivateRecoveryCooltime());
            }
        }
    }
    public void TakeDamage(float damage)
    {
        Hp = Hp - damage;

        //Debug.Log(Hp);
        if (Hp <= 0)
        {
            Hp = 0;
            UpdateHpBar();
            //GameOver시 수행할 코드
            UIController.Instance.OpenGameover();
            Debug.Log("GameOver");
        }

        if(!isMessageCooltime)
        {
            StartCoroutine(ActivateMessageCooltime());
        }
        
        UpdateHpBar();
    }
    public void Recovery(float recovery)
    {
        if(Hp + recovery > maxHp)
            Hp = maxHp;
        else
            Hp = Hp + recovery;
        Debug.Log(Hp);
        UpdateHpBar();
    }
    public void OnTriggerEnter(Collider other) 
    {
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
    }

    void UpdateHpBar()
    {
        currentScale = hpBar.transform.localScale;
        currentScale.y = Hp / maxHp;
        hpBar.transform.localScale = currentScale;
        if(hp != maxHp) setColor(currentHpRenderer, "Red");
        else setColor(currentHpRenderer, "Green");
    }

    void setColor(Renderer _renderer, string _color)
    {
        if(_color.Equals("Red")) _renderer.material = redMaterial;
        else if (_color.Equals("Green")) _renderer.material = greenMaterial;
    }

    protected IEnumerator ActivateRecoveryCooltime()
    {
        yield return new WaitForSeconds(recoveryTime);
        isRecoveryCooltime = false;
    }

    protected IEnumerator ActivateMessageCooltime()
    {
        messagePanel.SetActive(true);
        isMessageCooltime = true;
        SoundController.Instance.PlaySound2D("Alarm_nexus");
        yield return new WaitForSeconds(2f);
        messagePanel.SetActive(false);
        yield return new WaitForSeconds(2f);
        isMessageCooltime = false;
    }   
}
