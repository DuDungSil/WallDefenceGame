using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.OpenXR.Features.Interactions;

public class WallManager : StructureManager
{
    protected override void Start()
    {
        base.Start();
    }
    public override void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
    }
    public void temp()
    {
        Debug.Log("hi");
    }
}
