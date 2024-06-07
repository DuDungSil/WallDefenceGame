using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceWeaponUI : MonoBehaviour
{
    private DistanceWeanponControl distanceWeanponControl;

    public TMP_Text remainAmmoText;
    public TMP_Text MaxAmmoText;  

    void Start()
    {
        distanceWeanponControl = GetComponentInParent<DistanceWeanponControl>();
        if(distanceWeanponControl.useAmmo == true)
        {
            MaxAmmoText.text = distanceWeanponControl.maxAmmo.ToString();
        }

        distanceWeanponControl.onStatusChanged += UpdateUI;
        UpdateUI();
    }

    void UpdateUI()
    {
        if(distanceWeanponControl.useAmmo == true)
        {
            remainAmmoText.text = distanceWeanponControl.remainAmmo.ToString();
            if(distanceWeanponControl.remainAmmo == 0)
            {
                setColor(remainAmmoText, "Red");
            }
            else
            {
                setColor(remainAmmoText, "Green");
            }
        }
    }

    void setColor(TMP_Text tmp, string _color)
    {
        if(_color.Equals("Red")) tmp.color = new Color32(255, 0, 0, 255);
        else if (_color.Equals("Green")) tmp.color = new Color32(0, 255, 0, 255);
    }
}
