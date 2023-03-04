using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponHG : WeaponController {
    private void Init() {
        base.Init();
        base.ReloadTime = 2.1f;
        base.ikHandL.weight = 0;
        base.ikHandL.data.target = base.refIKHandL;
    }

     private void OnEnable() {
        Init();
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