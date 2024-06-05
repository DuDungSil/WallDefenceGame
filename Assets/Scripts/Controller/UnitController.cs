using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitController : Singleton<UnitController>
{
    public int maxUnitNum;
    public int assignedUnitNum;
    public TextMeshProUGUI maxUnitTxt;
    public TextMeshProUGUI assignedUnitTxt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        maxUnitTxt.text = maxUnitNum.ToString();
        assignedUnitTxt.text = assignedUnitNum.ToString();
    }
    public int GetReadyUnit()
    {
        return maxUnitNum - assignedUnitNum;
    }
    public bool AssignUnits(int neededUnits)
    {
        if(neededUnits + assignedUnitNum <= maxUnitNum)
        {
            assignedUnitNum += neededUnits;
            return true;
        }
        else
        {
            Debug.Log("Error : 가용 가능 유닛수 초과");
            // 가용 가능 유닛수 초과시 나올 UI
            return false;
        }
    }
    public bool RemoveUnits(int neededUnits)
    {
        if (assignedUnitNum - neededUnits >= 0)
        {
            assignedUnitNum -= neededUnits;
            return true;
        }
        else
        {
            Debug.Log("Error : 유닛 제거하면 배치된 유닛 수가 0보다 작아짐 - 어디선가 문제가 생겼다!");
            return false;
        }
    }
}
