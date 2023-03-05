using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponAR : WeaponController {
    private void Init() {
        base.Init();
        base.ReloadTime = 3.18f;
    }

    private void Awake() {
        Init();
    }

    private void OnEnable() {
        base.ikHandL.weight = 1;
        base.ikHandL.data.target = base.refIKHandL;
        WeaponUIController.instance.onAmmoEvent.Invoke(this.weaponSetting.currentAmmo, this.weaponSetting.maxAmmo);    // 탄 수 UI Invoke
        WeaponUIController.instance.onMagzineEvent.Invoke(base.weaponSetting.currentMagazine);  
    }

    private void Update() {
        base.UpdateFire();
        base.UpdateAim();
        base.UpdateReload();
    }

    public override IEnumerator OnReload() {
        base.ikHandL.weight = 0;

        yield return StartCoroutine(base.OnReload());

        if(!PlayerAnimatorController.instance.IsAim) {
            base.ikHandL.weight = 1;
        }

        yield return null;
    }

    
}