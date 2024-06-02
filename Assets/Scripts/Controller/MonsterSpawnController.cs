using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnController : Singleton<MonsterSpawnController>
{
    // Start is called before the first frame update
    public GameObject[] m_MonsterSpawnPoint;
    public GameObject[] m_Monster;
    public float m_RespawnTime;

    [SerializeField]
    public Round[] rounds;


    void Start()
    {
        TimeController.Instance.nightActionDelegate += MonsterSpawn;
    }


    IEnumerator MonsterSpawn()
    {
        int count = (int)(TimeController.Instance.m_nightTime / m_RespawnTime); // 밤시간 / 리스폰 대기시간 을 하여 몇번 몬스터를 스폰해야하는지 계산
        for(int i = 0; i < count; i++)
        {
            int randomPoint = Random.Range(0,m_MonsterSpawnPoint.Length); //몬스터 스포너 번호 랜덤하게 선택
            Vector3 spawnerPoint = m_MonsterSpawnPoint[randomPoint].transform.position; //선택된 번호의 몬스터 스포너의 위치 찾기
            GameObject monster = (GameObject)Instantiate(m_Monster[0], spawnerPoint, Quaternion.identity); //선택된 몬스터 스포너의 위치에 몬스터 생성
            yield return new WaitForSeconds(m_RespawnTime); //리스폰 시간동안 기다렸다가 코루틴
        }
        
    }
}
