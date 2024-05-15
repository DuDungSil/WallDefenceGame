using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider collision)
    {
        if(collision.transform.CompareTag("RightHand"))
        {
            UIController.Instance.CloseQuick();
        }
    }
}
