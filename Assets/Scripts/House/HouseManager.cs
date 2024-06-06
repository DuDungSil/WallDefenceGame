using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    public int IncreaseUnitsNum;
    // Start is called before the first frame update
    void Start()
    {
        UnitController.Instance.maxUnitNum += IncreaseUnitsNum;
    }
}
