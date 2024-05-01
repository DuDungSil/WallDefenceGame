using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnController : Singleton<MonsterSpawnController>
{
    // Start is called before the first frame update
    public GameObject[] m_MonsterSpawnPoint;
    public GameObject[] m_Monster;
    public float m_RespawnTime;
    private float RespawnTime;
    private int randomPoint;


    void Start()
    {
        RespawnTime = m_RespawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeController.Instance.IsNight)
        {
            if(RespawnTime < 0)
            {
                randomPoint = Random.Range(0,m_MonsterSpawnPoint.Length);
                MonsterSpawn(randomPoint);
                RespawnTime = m_RespawnTime;
            }
        }
        RespawnTime -= Time.deltaTime;
    }

    private void MonsterSpawn(int num)
    {
        Vector3 spawnerPoint = m_MonsterSpawnPoint[num].transform.position;
        GameObject monster = (GameObject)Instantiate(m_Monster[0], spawnerPoint, Quaternion.identity);
    }
}
