using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundData_", menuName = "New Round/Round Data")]
public class RoundData : ScriptableObject
{
    public int RoundNum => _roundNum;
    public GameObject[] Monsters => _monsters;
    public GameObject[] EliteMonsters => _eliteMonsters;
    public float RespawnTime => _respawnTime;
    public int MaxSpawnNum => _maxSpawnNum;

    [SerializeField] private int _roundNum;
    [SerializeField] private GameObject[] _monsters;
    [SerializeField] private GameObject[] _eliteMonsters;
    [SerializeField] private float _respawnTime;
    [SerializeField] private int _maxSpawnNum;


    public Round MakeRound()
    {
        return new Round(this);
    }
}
