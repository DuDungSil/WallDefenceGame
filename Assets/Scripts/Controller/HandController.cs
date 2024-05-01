using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : Singleton<HandController>
{
    public GameObject LeftUIController;
    public GameObject RightUIController;
    public GameObject LeftGrabController;
    public GameObject RightGrabController;

    public void SetUIController()
    {
        LeftGrabController.SetActive(false);
        RightGrabController.SetActive(false);
        LeftUIController.SetActive(true);
        RightUIController.SetActive(true);
    }

    public void SetGrabController()
    {
        LeftUIController.SetActive(false);
        RightUIController.SetActive(false);
        LeftGrabController.SetActive(true);
        RightGrabController.SetActive(true);
    }
}
