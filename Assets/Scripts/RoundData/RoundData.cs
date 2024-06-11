using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundData_", menuName = "New Round/Round Data")]
public class RoundData : ScriptableObject
{
    public int RoundNum => _roundNum;
    public GameObject[] Monsters => _monsters;
    public float[] MonstersWeights => _monstersWeights;
    public GameObject[] EliteMonsters => _eliteMonsters;
    public float RespawnTime => _respawnTime;
    public float RoundDayTime => _roundDayTime;
    public float RoundNightTime => _roundNightTime;
    public int MaxSpawnNum => _maxSpawnNum;
    public int MonsterNumVariable => _monsterNumVariable;

    [SerializeField] private int _roundNum;
    [SerializeField] private GameObject[] _monsters;
    [SerializeField] private float[]  _monstersWeights;
    [SerializeField] private GameObject[] _eliteMonsters;
    [SerializeField] private float _respawnTime;
    [SerializeField] private float _roundDayTime;
    [SerializeField] private float _roundNightTime;
    [SerializeField] private int _maxSpawnNum;
    [SerializeField] private int _monsterNumVariable; //한 웨이브에 소환될 몬스터수에 랜덤하게 영향을 끼칠 변수


    public Round MakeRound()
    {
        return new Round(this);
    }
}
