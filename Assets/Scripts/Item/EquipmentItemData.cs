using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentItemData : ItemData
{
    public GameObject EquipPrefab => _equipPrefab;
    public GameObject QuickSlotPrefab => _quickSlotPrefab;
    public int HandIndex => _handIndex;
    [SerializeField] private GameObject _equipPrefab;
    [SerializeField] private GameObject _quickSlotPrefab;
    
    [SerializeField]
    [Tooltip("0은 왼손, 1은 오른손")]
    private int _handIndex;
}
