using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnController : Singleton<MonsterSpawnController>
{
    // Start is called before the first frame update
    public GameObject[] m_MonsterSpawnPoint;
    public GameObject[] m_Portals;
    private int currentRound;
    private float[] MonsterSpawnCDF; // 몬스터 소환 누적확률분포
    private int avgMonsterNum; //한 웨이브에 소환될 몬스터 수의 평균
    private int currentWaveMonsterNum; //현재 웨이브에 소환할 몬스터 수
    private int MonsterNumVariable; //한 웨이브에 소환할 몬스터 수에 랜덤성을 부여하기 위한 변수
    private int currentSpawnedMonsterNum; //현재까지 소환된 몬스터의 수
    private int selectedSpawnPoint; //몬스터가 소환될 다리 위치를 랜덤하게 하나 잡기위함
    private Vector3 spawnPosition; //실제 몬스터가 스폰될 위치
    private MonsterManager monsterManager; //소환된 몬스터의 스크립트에 접근하기 위한 변수
    private int nexusPointNum; // 소환된 몬스터에게 줄 넥서스위치를 랜덤하게 하기 위한 변수

    [SerializeField]
    public Round[] rounds;


    void Start()
    {
        TimeController.Instance.nightActionDelegate += MonsterSpawn;
    }


    IEnumerator MonsterSpawn()
    {
        for(int i = 0; i < m_Portals.Length; i++)
        {
            m_Portals[i].SetActive(true);
        }
        currentRound = TimeController.Instance.currentRound - 1; //currentRound를 0부터 시작하는 코드에 맞게 실제 라운드에서 -1을 해줌
        int count = (int)(rounds[currentRound].Data.RoundNightTime / rounds[currentRound].Data.RespawnTime); // 밤시간 / 리스폰 대기시간 을 하여 몇번 몬스터를 스폰해야하는지 계산
        avgMonsterNum = rounds[currentRound].Data.MaxSpawnNum / count;
        MonsterSpawnCDF = GetCDF(rounds[currentRound].Data.MonstersWeights);
        
        for(int i = 0; i < count; i++)
        {
            if(i < count/2) // 아직 밤 시간의 절반이상 지나가지 않았다면 몬스터를 조금 적게 소환
            {
                MonsterNumVariable = Random.Range(0, rounds[currentRound].Data.MonsterNumVariable + 1);
                currentWaveMonsterNum = avgMonsterNum - MonsterNumVariable;
            }
            else if(i == count - 1) //마지막 웨이브에는 남은 모든 몬스터가 소환됨
            {
                currentWaveMonsterNum = rounds[currentRound].Data.MaxSpawnNum - currentSpawnedMonsterNum;
                if(rounds[currentRound].Data.EliteMonsters.Length >= 1)
                {
                    for(int k = 0; k < rounds[currentRound].Data.EliteMonsters.Length; k++)
                    {
                        // 엘리트 몬스터 소환 위치는 가운데 다리로 정함(바꿔도 됨)
                        selectedSpawnPoint = 1;
                        // 엘리트 몬스터 소환
                        GameObject monster = (GameObject)Instantiate(rounds[currentRound].Data.EliteMonsters[k], SelectSpawnPosition(m_MonsterSpawnPoint[selectedSpawnPoint * 2],m_MonsterSpawnPoint[selectedSpawnPoint * 2 + 1]), Quaternion.identity);
                        // 소환한 몬스터에게 nexusPoint(공격할 대상)을 주기 위함.
                        monsterManager = monster.GetComponent<MonsterManager>(); 
                        if(monsterManager != null)
                        {
                            nexusPointNum = Random.Range(0,6);
                            monsterManager.nexusPoint = NexusController.Instance.attackPoint[nexusPointNum];
                        }
                        else
                            Debug.Log("Error : monsterManager is null");
                    }
                }
            }
            else if(i > count/2) //밤 시간이 절반이상 지나갔다면 몬스터를 조금 많이 소환
            {
                MonsterNumVariable = Random.Range(0, rounds[currentRound].Data.MonsterNumVariable / 2 + 1);
                currentWaveMonsterNum = avgMonsterNum + MonsterNumVariable;
            }
            currentSpawnedMonsterNum += currentWaveMonsterNum; //현재까지 소환된 몬스터 숫자 기록
            for(int k = 1; k <= currentWaveMonsterNum; k++)
            {
                selectedSpawnPoint = Random.Range(0,3); //3개의 다리중 하나를 결정
                // 소환될 몬스터 인덱스 결정
                int monsterIndex = SelectSpawnMonster(MonsterSpawnCDF);
                // 몬스터 소환
                GameObject monster = (GameObject)Instantiate(rounds[currentRound].Data.Monsters[monsterIndex], SelectSpawnPosition(m_MonsterSpawnPoint[selectedSpawnPoint * 2],m_MonsterSpawnPoint[selectedSpawnPoint * 2 + 1]),Quaternion.identity);
                
                //소환한 몬스터에게 nexusPoint(공격할 대상)을 주기 위함.
                monsterManager = monster.GetComponent<MonsterManager>(); 
                if(monsterManager != null)
                {
                    nexusPointNum = Random.Range(0,6);
                    monsterManager.nexusPoint = NexusController.Instance.attackPoint[nexusPointNum];
                }
                else
                    Debug.Log("Error : monsterManager is null");
            }
            //리스폰 타임 기다리기
            yield return new WaitForSeconds(rounds[currentRound].Data.RespawnTime);
        }
        for(int i = 0; i < m_Portals.Length; i++)
        {
            m_Portals[i].GetComponent<ParticleSystem>().Stop();
        }
    }

    //스폰 위치를 스폰가능한 직선상의 위치중 랜덤하게 하나 정해줌
    Vector3 SelectSpawnPosition(GameObject spawnPos1, GameObject spawnPos2)
    {
        float randomNum = Random.Range(0f,1f);
        return Vector3.Lerp(spawnPos1.transform.position,spawnPos2.transform.position,randomNum);
    }

    int SelectSpawnMonster(float[] MonsterSpawnCDF)
    {
        float randomNum = Random.Range(0f,1f);
        for(int i = 0; i < MonsterSpawnCDF.Length; i++)
        {    
            if(randomNum <= MonsterSpawnCDF[i]) return i;
            else if(i == MonsterSpawnCDF.Length - 1) return i;
        }
        return -1; // 오류발생
    }

    // 몬스터 가중치 배열을 받아서 누적확률분포 배열로 만들어줌
    public float[] GetCDF(float[] values)
    {
        float sum = 0f;
        int length = values.Length;

        // Calculate the sum of all values
        for (int i = 0; i < length; i++)
        {
            sum += values[i];
        }

        // Create a probability array and cumulative distribution array
        float[] probabilities = new float[length];
        float[] cumulativeDistribution = new float[length];

        float cumulativeSum = 0f;

        // Normalize values to probabilities and calculate cumulative distribution
        for (int i = 0; i < length; i++)
        {
            probabilities[i] = values[i] / sum;
            cumulativeSum += probabilities[i];
            cumulativeDistribution[i] = cumulativeSum;
        }

        return cumulativeDistribution;
    }
}
