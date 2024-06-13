using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AssignUnitStructureManager : StructureManager
{
    public GameObject Units;
    protected bool isAssigned = false;
    public bool IsAssigned{
        get{ return isAssigned; }
        protected set{isAssigned = value;}
    }
    [SerializeField]
    protected int neededUnits;
    public int NeededUnits{
        get{ return neededUnits;} 
        protected set{ neededUnits = value;}
    }

    public void AssignedChange() //AssignBtn과 RemoveBtn의 이벤트함수
    {
        if(isAssigned) //RemoveBtn listener
        {
            if(UnitController.Instance.RemoveUnits(NeededUnits))
            {
                isAssigned = false;
                if(Units != null)
                {
                    Units.SetActive(false);
                }
            }
        }
        else //AssignBtn listener
        {
            if(UnitController.Instance.AssignUnits(NeededUnits))
            {
                isAssigned = true;
                if(Units != null)
                {
                    Units.SetActive(true);
                }
            }
        }
    }
    public override void Selfdestroy() // DestroyBtn의 이벤트함수
    {
        Debug.Log("부수기 작동");
        // 할당된 unit을 UnitController에 돌려주는 코드
        if(isAssigned)
            UnitController.Instance.RemoveUnits(NeededUnits);

        SoundController.Instance.PlaySound2D("Building_destroy");
        Destroy(gameObject);
    }
    public override GameObject SelfUpgrade() //UpgradeBtn의 이벤트함수
    {
        Debug.Log("업그레이드 작동");
        GameObject upgradeObj = Instantiate(nextUpgrade.Data.prefab, gameObject.transform.position, gameObject.transform.rotation);
        ResourceDatabase.Instance.DecreaseResource(nextUpgrade.craftNeedItems, nextUpgrade.Data.craftNeedItemCount);
        // 할당된 unit을 UnitController에 돌려주는 코드
        if(isAssigned)
            UnitController.Instance.RemoveUnits(NeededUnits);

        
        return upgradeObj;
    }


}