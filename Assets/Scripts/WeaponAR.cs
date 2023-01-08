using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAR : MonoBehaviour {
    [SerializeField] private WeaponSetting weaponSetting;


    private void Init() {
        this.weaponSetting.currentAmmo = this.weaponSetting.maxAmmo;
        this.weaponSetting.currentMagazine = this.weaponSetting.maxMagazine;
        this.weaponSetting.isAuto = true;
        this.weaponSetting.weaponType = WeaponType.AR;
    }

    private void Awake() {
        Init();
    }
}
