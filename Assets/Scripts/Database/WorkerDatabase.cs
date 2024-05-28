using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerDatabase : Singleton<WorkerDatabase>
{
    public int initialWorker = 4;
    int totalWorker;
    int assignedWorker;

    void Start()
    {
        // 초기 일꾼 수 설정
        totalWorker = initialWorker;
        assignedWorker = 0;
    }

    void Update()
    {
        
    }

    public void AddWorker(int num)
    {
        totalWorker += num;
    }
    public void DeleteWorker(int num)
    {
        totalWorker -= num;
        if(totalWorker < initialWorker) Debug.Log("오류. 초기 worker수보다 작아졌습니다.");
    }
    public int RemainWorker()
    {
        return totalWorker - assignedWorker;
    }
    public bool IsAssignableToWorker()
    {
        if(totalWorker < assignedWorker) return false;
        return true;
    }
}
