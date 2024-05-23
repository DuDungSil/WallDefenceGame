using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class SettingSlotManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject CraftSlotsParent;
    [SerializeField] 
    private int equipDBIndex;
    [SerializeField] 
    private int numOfsettingSlots = 3;

    private List<EquipmentItem> equipDB;
    private SettingSlot[] settingSlots;
    private int currentIndex;


    void Start()
    {
        equipDB = EquipmentDatabase.Instance.getDatabase(equipDBIndex);
        settingSlots = CraftSlotsParent.GetComponentsInChildren<SettingSlot>();    
        currentIndex = 0;
        UpdateUI();
        EquipmentDatabase.Instance.onItemChanged += UpdateUI;
    }

    
    void Update()
    {

    }

    void SettingSlotUpdate()
    {
        if(currentIndex - 1 < 0) settingSlots[0].gameObject.SetActive(false);
        else
        {
            settingSlots[0].SlotUpadate(equipDB[currentIndex - 1].QuickSlotPrefab);
            settingSlots[0].gameObject.SetActive(true);
        }
        
        if(equipDB.Count != 0) 
        {
            settingSlots[1].SlotUpadate(equipDB[currentIndex].QuickSlotPrefab);
            settingSlots[1].gameObject.SetActive(true);
        }
        else settingSlots[1].gameObject.SetActive(false);

        if(currentIndex + 1 > equipDB.Count - 1) settingSlots[2].gameObject.SetActive(false);
        else
        {
            settingSlots[2].SlotUpadate(equipDB[currentIndex + 1].QuickSlotPrefab);
            settingSlots[2].gameObject.SetActive(true);
        }
        
    }

    public void previousPage()
    {
        if(currentIndex > 0)
        {
            currentIndex--;
            SettingSlotUpdate();
            QuickSlotsDatabase.Instance.setQuickslots(equipDBIndex, equipDB[currentIndex]);
        } 
        // 이전 인덱스로 변경
        // 퀵슬롯 무기 변경
    }

    public void nextPage()
    {
        if(currentIndex < equipDB.Count - 1)
        {
            currentIndex++;
            SettingSlotUpdate();
            QuickSlotsDatabase.Instance.setQuickslots(equipDBIndex, equipDB[currentIndex]);
        } 
    }

    private void UpdateUI()
    {
        equipDB = EquipmentDatabase.Instance.getDatabase(equipDBIndex);
        if(equipDB.Count != 0) QuickSlotsDatabase.Instance.setQuickslots(equipDBIndex, equipDB[currentIndex]);
        SettingSlotUpdate();
    }
    
}
