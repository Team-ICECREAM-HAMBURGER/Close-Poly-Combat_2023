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
    public WeaponType weaponType;   // 무기 종류

    [Header("Selector")]
    public bool isAuto;             // 자동
    public bool isSemi;             // 반자동
    public bool isSingle;           // 단발

    [Space(10f)]

    [Header("Ammo")]
    public int currentAmmo;         // 현재 탄약
    public int maxAmmo;             // 최대 탄약

    [Space(10f)]

    [Header("Magazine")]
    public int currentMagazine;     // 현재 탄창
    public int maxMagazine;         //최대 탄창

    [Space(10f)]

    [Header("Specification")]
    public int damage;              // 공격력
    public float attackRate;        // RPM
    public float attackDistance;    // 사거리
    public float spreadRange;       // 탄 퍼짐 정도
}
