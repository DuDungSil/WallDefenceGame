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
            Debug.Log("Error : UnitsNum over Max");
            // Unit이 Max를 넘겼을 경우 나올 UI
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
            Debug.Log("Error : 뭔가 잘못됐다.");
            return false;
        }
    }
    public bool isAssignable(int neededUnits)
    {
        if(neededUnits + assignedUnitNum <= maxUnitNum)
            return true;
        else
            return false;
    }
}
