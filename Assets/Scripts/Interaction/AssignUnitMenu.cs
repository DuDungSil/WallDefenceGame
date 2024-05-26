using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssignUnitMenu : MonoBehaviour
{
    public Button removeBtn;
    public Button assignBtn;
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
    public void SetAlreadyAssignUI(StructureManager structureManager) // structure를 select했을 때 실행되는 함수(선택된 structure에 맞는 값들로 UI구성 및 해당 객체에 대한 버튼 리스너)
    {
        removeBtn.gameObject.SetActive(true);
        assignUnitsImg.SetActive(true);
        assignUnitsNum.text = structureManager.neededUnits.ToString();
            
        assignBtn.gameObject.SetActive(false);
        readyUnitsImg.SetActive(false);
        neededUnitsImg.SetActive(false);
        
        removeBtn.onClick.RemoveAllListeners();
        removeBtn.onClick.AddListener(structureManager.AssignedChange);
        removeBtn.onClick.AddListener(() => SetNeedAssignUI(structureManager));
    }
    public void SetNeedAssignUI(StructureManager structureManager) // structure를 select했을 때 실행되는 함수(선택된 structure에 맞는 값들로 UI구성 및 해당 객체에 대한 버튼 리스너)
    {
        assignBtn.gameObject.SetActive(true);
        readyUnitsImg.SetActive(true);
        neededUnitsImg.SetActive(true);
        readyUnitsNum.text = UnitController.Instance.GetReadyUnit().ToString();
        neededUnitsNum.text = structureManager.neededUnits.ToString();

        removeBtn.gameObject.SetActive(false);
        assignUnitsImg.SetActive(false);

        assignBtn.onClick.RemoveAllListeners();
        assignBtn.onClick.AddListener(structureManager.AssignedChange);
        assignBtn.onClick.AddListener(() => SetAlreadyAssignUI(structureManager));
    }
    
}
