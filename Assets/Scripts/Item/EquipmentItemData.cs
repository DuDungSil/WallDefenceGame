using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentItemData : ItemData
{
    public GameObject Prefab => _prefab;
    [SerializeField] private GameObject _prefab;
}
