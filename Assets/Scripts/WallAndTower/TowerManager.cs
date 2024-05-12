using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : StructureManager
{
    public GameObject projectileSpwanPos;
    public GameObject prefab;
    public float shootDelay;
    public float m_speed;
    private bool shootActivate = true;
    public virtual void OnTriggerEnter(Collider other) {
        Debug.Log("몬스터 출현");
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Vector3 monsterPos = other.transform.position;
            Shoot(monsterPos);
        } 
    }
    void Shoot(Vector3 monsterPos)
    {
        shootActivate = false;
        StartCoroutine(ShootDelay(shootDelay));
        // 투사체 소환
        GameObject spawnedProjectile = Instantiate(prefab, projectileSpwanPos.transform.position, projectileSpwanPos.transform.rotation);
        // 데미지 설정
        spawnedProjectile.GetComponent<ProjectileControl>().damage = 10;
        Vector3 direction = (monsterPos - projectileSpwanPos.transform.position).normalized;
        spawnedProjectile.transform.rotation = Quaternion.LookRotation(direction);
        // 물리속성 설정
        Rigidbody r = spawnedProjectile.GetComponent<Rigidbody>();
        r.AddForce(projectileSpwanPos.transform.forward * m_speed, ForceMode.Impulse);
    }
    IEnumerator ShootDelay(float _shootDelay)
    {
        yield return new WaitForSeconds(_shootDelay);
        shootActivate = true;
    }
}
