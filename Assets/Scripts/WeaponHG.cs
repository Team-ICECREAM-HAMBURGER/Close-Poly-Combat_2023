using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponHG : WeaponController {
    private void Init() {
        base.Init();
        base.reloadTime = 2.1f;
    }

     private void Awake() {
        Init();
    }

    private void Update() {
        base.UpdateFire();
        UpdateAim();
        base.UpdateReload();
        StartCoroutine(UpdateChangeWeapon());
    }

    private void OnEnable() {
        UpdateMagazineHUD(this.weaponSetting.currentMagazine);
        this.onAmmoEvent.Invoke(this.weaponSetting.currentAmmo, this.weaponSetting.maxAmmo);    // 탄 수 UI Invoke
    }

    public override IEnumerator UpdateChangeWeapon() {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !this.isReload) {    // 주무기 선택
            PlayerAnimatorController.instance.animator.SetTrigger("Holster_AR");

            yield return new WaitForSeconds(0.5f);

            this.playerAnimator.runtimeAnimatorController = this.playerAR;
            this.nextWeapon.SetActive(true);


            for (int i = 0; i < this.magazineList.Count; ++i) { // 다 끄기
                this.magazineList[i].SetActive(false);
            }

            gameObject.SetActive(false);
        }
    }

    private void UpdateAim() {
        if (this.isReload) {
            PlayerAnimatorController.instance.IsAim = false;
            PlayerAnimatorController.instance.Aiming = 0;
        }
        else {
            base.UpdateAim();
        }
    }
}