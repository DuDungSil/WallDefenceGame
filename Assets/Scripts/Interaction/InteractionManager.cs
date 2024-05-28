using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionManager : Singleton<InteractionManager>
{
    public GameObject interactionCanvas;

    public void forBuildingInteraction(BaseInteractionEventArgs args)
    {
        if(interactionCanvas.activeSelf)
        {
            if (args is SelectEnterEventArgs selectArgs)
            {
                var selectedObject = selectArgs.interactableObject;
                interactionCanvas.GetComponent<InteractionUIControl>().setInteractionObject(selectedObject.transform.gameObject);
            }
        }
    }
}
