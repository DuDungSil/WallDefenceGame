using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR.Features.Interactions;

public class WallManager : AssignUnitStructureManager
{
    private float time;
    [SerializeField]
    protected float repairTime;
    [SerializeField]
    protected float repairAmount;
    protected override void Start()
    {
        base.Start();
    }
    protected virtual void Update()
    {
        if(isAssigned)
        {
            time += Time.deltaTime;
            if(time > repairTime)
            {
                Repair(repairAmount);
                time = 0;
            }
        }
    }
    public override void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
    }
    public void temp()
    {
        Debug.Log("hi");
    }
}
