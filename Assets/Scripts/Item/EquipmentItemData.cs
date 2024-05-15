using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentItemData : ItemData
{
    public GameObject EquipPrefab => _equipPrefab;
    public GameObject QuickSlotPrefab => _quickSlotPrefab;
    [SerializeField] private GameObject _equipPrefab;
    [SerializeField] private GameObject _quickSlotPrefab;
}
