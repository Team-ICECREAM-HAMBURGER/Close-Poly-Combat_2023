using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    AR = 0,
    SMG = 1,
    SG = 2,
    HG = 3,
}

[System.Serializable]
public struct WeaponSetting {
    public bool isAuto;             // 자동
    public bool isSemi;             // 반자동
    public bool isSingle;           // 단발
    public int currentAmmo;         // 현재 탄약
    public int maxAmmo;             // 최대 탄약
    public int currentMagazine;     // 현재 탄창
    public int maxMagazine;         //최대 탄창
    public int damage;              // 공격력
    public float attackRate;        // RPM
    public float attackDistance;    // 사거리
    public WeaponType weaponType;   // 무기 종류
}
