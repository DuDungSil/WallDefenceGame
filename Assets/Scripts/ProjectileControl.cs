using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour
{
    public float m_damage;
    public float projectileLifeTime;

    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Destroy(gameObject);
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterTime()
    {
        // lifetime 만큼 대기
        yield return new WaitForSeconds(projectileLifeTime);

        // 총알 제거
        Destroy(gameObject);
    }

    // 생성시 어느 방향으로 어떻게 날아갈지 지정

    // 고유 데미지를 가지고있음, 생성시 설정됨

    // 몬스터에 맞거나, 몇초뒤에 자동 제거
}