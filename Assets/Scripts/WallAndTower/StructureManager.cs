using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public bool isAssigned = false;
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
}