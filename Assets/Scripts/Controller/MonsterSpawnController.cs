using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnController : Singleton<MonsterSpawnController>
{
    // Start is called before the first frame update
    public GameObject[] m_MonsterSpawnPoint;
    private int currentRound;
    private int avgMonsterNum; //한 웨이브에 소환될 몬스터 수의 평균
    private int currentWaveMonsterNum; //현재 웨이브에 소환할 몬스터 수
    private int MonsterNumVariable; //한 웨이브에 소환할 몬스터 수에 랜덤성을 부여하기 위한 변수
    private int currentSpawnedMonsterNum; //현재까지 소환된 몬스터의 수
    private int selectedSpawnPoint; //몬스터가 소환될 다리 위치를 랜덤하게 하나 잡기위함
    private Vector3 spawnPosition; //실제 몬스터가 스폰될 위치

    [SerializeField]
    public Round[] rounds;


    void Start()
    {
        TimeController.Instance.nightActionDelegate += MonsterSpawn;
    }


    IEnumerator MonsterSpawn()
    {
        currentRound = TimeController.Instance.currentRound - 1;
        int count = (int)(rounds[currentRound].Data.RoundNightTime / rounds[currentRound].Data.RespawnTime); // 밤시간 / 리스폰 대기시간 을 하여 몇번 몬스터를 스폰해야하는지 계산
        avgMonsterNum = rounds[currentRound].Data.MaxSpawnNum / count;
        
        for(int i = 0; i < count; i++)
        {
            if(i < count/2)
            {
                MonsterNumVariable = Random.Range(0, rounds[currentRound].Data.MonsterNumVariable + 1);
                currentWaveMonsterNum = avgMonsterNum - MonsterNumVariable;
            }
            else if(i == count - 1)
            {
                currentWaveMonsterNum = rounds[currentRound].Data.MaxSpawnNum - currentSpawnedMonsterNum;
            }
            else if(i > count/2)
            {
                MonsterNumVariable = Random.Range(0, rounds[currentRound].Data.MonsterNumVariable / 2 + 1);
                currentWaveMonsterNum = avgMonsterNum + MonsterNumVariable;
            }
            currentSpawnedMonsterNum += currentWaveMonsterNum; //현재까지 소환된 몬스터 숫자 기록
            for(int k = 1; k <= currentWaveMonsterNum; k++)
            {
                selectedSpawnPoint = Random.Range(0,3);
                //여기의 rounds[currentRound].Data.Monsters[0] 부분을 수정해서 다른 몬스터도 소환 가능.
                GameObject monster = (GameObject)Instantiate(rounds[currentRound].Data.Monsters[0], SelectSpawnPosition(m_MonsterSpawnPoint[selectedSpawnPoint * 2],m_MonsterSpawnPoint[selectedSpawnPoint * 2 + 1]),Quaternion.identity);
            }
            yield return new WaitForSeconds(rounds[currentRound].Data.RespawnTime);
        }
    }

    Vector3 SelectSpawnPosition(GameObject spawnPos1, GameObject spawnPos2)
    {
        float randomNum = Random.Range(0f,1f);
        return Vector3.Lerp(spawnPos1.transform.position,spawnPos2.transform.position,randomNum);
    }
}
