using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : StructureManager
{
    public int IncreaseUnitsNum;
    // Start is called before the first frame update
    new void Start()
    {
        UnitController.Instance.maxUnitNum += IncreaseUnitsNum;
    }

}
