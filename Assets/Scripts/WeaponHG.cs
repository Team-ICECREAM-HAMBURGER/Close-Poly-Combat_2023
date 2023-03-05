using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponHG : WeaponController {
    private void Init() {
        base.Init();
        base.ReloadTime = 2.1f;
    }

    private void Awake() {
        Init();
    }

     private void OnEnable() {
        base.ikHandL.weight = 0;
        base.ikHandL.data.target = base.refIKHandL;    
        WeaponUIController.instance.onAmmoEvent.Invoke(this.weaponSetting.currentAmmo, this.weaponSetting.maxAmmo);    // 탄 수 UI Invoke
        WeaponUIController.instance.onMagzineEvent.Invoke(base.weaponSetting.currentMagazine);    
    }

    private void Update() {
        base.UpdateFire();
        UpdateAim();
        base.UpdateReload();
    }

    private void UpdateAim() {
        if (this.IsReload) {
            PlayerAnimatorController.instance.IsAim = false;
            PlayerAnimatorController.instance.Aiming = 0;
        }
        else {
            base.UpdateAim();
        }
    }
}