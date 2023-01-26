using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponAR : WeaponController {
    private void Init() {
        base.Init();
        base.reloadTime = 3.18f;
    }

    private void Awake() {
        Init();
    }

    private void Update() {
        base.UpdateFire();
        base.UpdateAim();
        base.UpdateReload();
        UpdateChangeWeapon();
    }

    private void OnEnable() {
        base.OnMagHUD();
    }

    public override void UpdateChangeWeapon() {
        if (Input.GetKeyDown(KeyCode.Alpha2) && !this.isReload) {    // 보조 무기 선택
            this.playerAnimator.runtimeAnimatorController = this.playerHG;
            this.nextWeapon.SetActive(true);

            for (int i = 0; i < this.magazineList.Count; ++i) { // 다 끄기
                this.magazineList[i].SetActive(false);
            }

            gameObject.SetActive(false);
        }
    }
}