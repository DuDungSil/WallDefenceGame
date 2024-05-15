using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour
{
    public Image Image;
    public TMP_Text text_Name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setItem(Image _Image, TMP_Text _text_Name)
    {
        Image = _Image;
        text_Name = _text_Name;
    }

}
