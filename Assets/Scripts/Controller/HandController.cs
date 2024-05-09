using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : Singleton<HandController>
{
    public GameObject LeftUIController;
    public GameObject RightUIController;
    public GameObject LeftGrabController;
    public GameObject RightGrabController;
    public GameObject FixedRightHand;

    bool isUIController = false;
    bool isGrabController = true;
    bool isFixedHand = false;

    public void SetUIController()
    {
        LeftGrabController.SetActive(false);
        RightGrabController.SetActive(false);
        LeftUIController.SetActive(true);
        RightUIController.SetActive(true);
        isUIController = true;
        isGrabController = false;
    }

    public void SetGrabController()
    {
        LeftUIController.SetActive(false);
        RightUIController.SetActive(false);
        LeftGrabController.SetActive(true);
        RightGrabController.SetActive(true);
        isUIController = false;
        isGrabController = true;
    }

    // 장비 장착
    public void SetRightHandEquipment(int index)
    {
        RightGrabController.SetActive(false);
        FixedRightHand.SetActive(true);
        FixedRightHand.GetComponent<FixedRightHandControl>().setEquipment(index);
        isFixedHand = true;
    }

    // 맨손 변경
    public void SetBareHands()
    {
        if(isFixedHand == true) RightGrabController.transform.position = FixedRightHand.transform.position;
        RightGrabController.SetActive(true);
        FixedRightHand.SetActive(false);
        isFixedHand = false;
    }

}
