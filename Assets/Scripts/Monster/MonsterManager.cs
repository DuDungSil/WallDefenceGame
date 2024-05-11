using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.OpenXR.Features.Interactions;

public abstract class MonsterManager : MonoBehaviour
{
    [SerializeField]
    protected int hp;
    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    public Animator m_animator;
    public virtual void TakeDamage(int damage)
    {
        Hp = Hp - damage;
        if (Hp < 0)
        {
            StartCoroutine(Death());
        }
    }
    public IEnumerator Death()
    {
        m_animator.SetTrigger("DeathTrigger");
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
    public abstract void OnTriggerEnter(Collider other);
}
