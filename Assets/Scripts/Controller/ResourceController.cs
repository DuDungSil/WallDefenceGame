using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    private float timer;
    public float Timer{get{return timer;}}
    [SerializeField]
    private float resourceGetTime;
    private int assignedWoodNum;
    private int assignedStoneNum;

    void Start()
    {
        timer = 0f;
        assignedWoodNum = 0;
        assignedStoneNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > resourceGetTime)
        {
            //wood 개수, stone 개수 증가
            Debug.Log("wood 개수: " + assignedWoodNum.ToString());
            Debug.Log("stone 개수: " + assignedStoneNum.ToString());
            timer = 0f;
        }
    }

    public void AddAssignedResource(string resourceType)
    {
        if(resourceType == "wood")
        {
            assignedWoodNum++;
        }
        else if(resourceType == "stone")
        {
            assignedStoneNum++;
        }
    }
}
