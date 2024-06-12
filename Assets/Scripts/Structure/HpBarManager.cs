using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarManager : MonoBehaviour
{
    [SerializeField]
    private GameObject rootObject;
    private StructureManager rootStructureManager;

    [SerializeField]
    private GameObject currentHpObject;
    private Vector3 currentScale;
    public Material redMaterial;
    public Material greenMaterial;
    public Renderer currentHpRenderer;
    void Start() 
    {
        if(rootObject != null)
            rootStructureManager = rootObject.GetComponent<StructureManager>();
        else
            Debug.Log("One of Structures is null");

        rootStructureManager.onStatusChanged += UpdateUI;
        UpdateUI();
    }

    void UpdateUI()
    {
        currentScale = currentHpObject.transform.localScale;
        currentScale.y = rootStructureManager.Hp / rootStructureManager.MaxHp;
        currentHpObject.transform.localScale = currentScale;
        if(rootStructureManager.Hp != rootStructureManager.MaxHp) setColor(currentHpRenderer, "Red");
        else setColor(currentHpRenderer, "Green");
    }

    void setColor(Renderer _renderer, string _color)
    {
        if(_color.Equals("Red")) _renderer.material = redMaterial;
        else if (_color.Equals("Green")) _renderer.material = greenMaterial;
    }

}
