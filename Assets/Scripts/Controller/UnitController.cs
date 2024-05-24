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
}
