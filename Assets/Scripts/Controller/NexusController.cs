using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusController : Singleton<NexusController>
{
    public GameObject[] attackPoint;
    [SerializeField]
    private float maxHp;
    private float hp;
    private Vector3 currentScale;
    public GameObject hpBar;
    public float Hp
    {
        get { return hp; }
        private set { hp = value; }
    }
    void Start()
    {
        Hp = maxHp;
    }
    public void TakeDamage(float damage)
    {
        Hp = Hp - damage;
        Debug.Log(Hp);
        if (Hp <= 0)
        {
            //GameOver시 수행할 코드
            UIController.Instance.OpenGameover();
            Debug.Log("GameOver");
        }
        currentScale = hpBar.transform.localScale;
        currentScale.y = Hp / maxHp;
        hpBar.transform.localScale = currentScale;
    }
    public void OnTriggerEnter(Collider other) {
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
}
