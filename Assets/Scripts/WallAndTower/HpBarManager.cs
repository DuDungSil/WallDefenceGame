using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarManager : MonoBehaviour
{
    [SerializeField]
    private GameObject rootObject;
    private StructureManager rootStructureManager;
    private float lastHp;
    private float maxHp;
    [SerializeField]
    private GameObject currentHpObject;
    private Vector3 currentScale;
    void Start() 
    {
        if(rootObject != null)
            rootStructureManager = rootObject.GetComponent<StructureManager>();
        else
            Debug.Log("One of Structures is null");
        lastHp = rootStructureManager.Hp;
        maxHp = rootStructureManager.MaxHp;
    }
    // Update is called once per frame
    void Update()
    {
        if(rootStructureManager.Hp != lastHp)
        {
            currentScale = currentHpObject.transform.localScale;
            currentScale.y = rootStructureManager.Hp / maxHp;
            currentHpObject.transform.localScale = currentScale;
            lastHp = rootStructureManager.Hp;
        }
    }
}
