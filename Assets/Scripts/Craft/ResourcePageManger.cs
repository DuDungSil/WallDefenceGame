using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePageManger : MonoBehaviour
{

    [SerializeField] 
    private GameObject go_SlotsParent;  // Slot들의 부모인 Grid Setting
    List<ResourceItem> resourceDatabase;  // 리소스 데이터베이스
    private ResourceSlot[] resourceSlots;

    public int numOfSlots;  // 페이지당 슬롯 수

    int currentPage;
    int lastPage;

    // Start is called before the first frame update
    void Start()
    {
        resourceDatabase = ResourceDatabase.Instance.getDatabase();
        resourceSlots = go_SlotsParent.GetComponentsInChildren<ResourceSlot>();
        currentPage = 0;
        lastPage = (resourceDatabase.Count-1) / numOfSlots;

        if(resourceDatabase.Count == 0)
        {
            for(int i = 0; i < numOfSlots; i++)
                resourceSlots[i].gameObject.SetActive(false);
        }
        else slotsUpdate();

        ResourceDatabase.Instance.onItemChanged += UpdateUI;
    }

    void setStartPage()
    {
        currentPage = 0;
        slotsUpdate();
    }
    
    void slotsUpdate() // 동적으로 현재 페이지에 보여줄 slot들 업데이트 ( 한페이지 numOfSlots 개 )
    {
        int numActiveSlot = numOfSlots;
        int startIndex = currentPage * numOfSlots;

        if(currentPage == lastPage)
        {
            numActiveSlot = resourceDatabase.Count % numOfSlots == 0 ? numOfSlots : resourceDatabase.Count % numOfSlots;
        }

        for(int i = 0; i < numActiveSlot; i++)
        {
            resourceSlots[i].gameObject.SetActive(true);
            resourceSlots[i].setItem(resourceDatabase[startIndex + i], resourceDatabase[startIndex + i].Amount);
        }

        for(int i = numActiveSlot; i < numOfSlots; i++)
        {
            resourceSlots[i].gameObject.SetActive(false);
        }

    }

    public void previousPage()
    {
        if(currentPage > 0)
        {
            currentPage--;
            slotsUpdate();
        } 
        // 0번째로 가면 이전버튼 비활성화
    }

    public void nextPage()
    {
        if(currentPage < lastPage)
        {
            currentPage++;
            slotsUpdate();
        } 
        // 마지막 페이지면 다음버튼 비활성화
    }
   
    // UI 업데이트 메서드
    private void UpdateUI()
    {
        resourceDatabase = ResourceDatabase.Instance.getDatabase();
        lastPage = (resourceDatabase.Count-1) / numOfSlots;
        slotsUpdate();
    }

    // 리소스 데이터베이스 참조
    
    // 현재 페이지 변수 0으로 초기화, 최대 페이지 갯수 계산

    // 페이지 버튼 좌 우 활성화/비활성화

    // 페이지 변경 함수
}
