using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponItem : WeaponItem
{    

    public RangedWeaponItemData rangeWeaponData { get; private set; }

    // 남은 원거리무기 탄창 수
    public int remainAmmo;
    // 마지막 슛 시간
    public float lastShootTime;
    // 마지막 쿨타임 시간
    public float lastTime;
    // 장착 해제시 쿨타임이었는지
    public bool isCoolTime;

    public RangedWeaponItem(RangedWeaponItemData data) : base(data) 
    { 
        rangeWeaponData = data;
        remainAmmo = -1;
        lastShootTime = 0f;
        lastTime = 0f;
        isCoolTime = false;
    }
    
} 
