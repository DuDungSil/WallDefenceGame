using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssignUnitMenu : MonoBehaviour
{
    public GameObject removeBtn;
    public GameObject assignBtn;
    public GameObject readyUnitsImg;
    public GameObject neededUnitsImg;
    public GameObject assignUnitsImg;
    public TextMeshProUGUI readyUnitsNum;
    public TextMeshProUGUI neededUnitsNum;
    public TextMeshProUGUI assignUnitsNum;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void alreadyAssign(StructureManager structureManager)
    {
        removeBtn.SetActive(true);
        assignUnitsImg.SetActive(true);
        assignUnitsNum.text = structureManager.neededUnits.ToString();

        assignBtn.SetActive(false);
        readyUnitsImg.SetActive(false);
        neededUnitsImg.SetActive(false);
    }
    public void needAssign(StructureManager structureManager)
    {
        assignBtn.SetActive(true);
        readyUnitsImg.SetActive(true);
        neededUnitsImg.SetActive(true);
        readyUnitsNum.text = UnitController.Instance.GetReadyUnit().ToString();
        neededUnitsNum.text = structureManager.neededUnits.ToString();

        removeBtn.SetActive(false);
        assignUnitsImg.SetActive(false);
    }
}
