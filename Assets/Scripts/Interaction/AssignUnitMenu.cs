using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssignUnitMenu : MonoBehaviour
{
    public TextMeshProUGUI readyUnitsNum;
    // Start is called before the first frame update
    void Start()
    {
        readyUnitsNum.text = UnitController.Instance.GetReadyUnit().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
