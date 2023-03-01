using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSG : WeaponController {

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
        StartCoroutine(UpdateChangeWeapon());
    }

    private void OnEnable() {
        base.OnMagHUD();
    }
    
    public override IEnumerator UpdateChangeWeapon() {
        if (Input.GetKeyDown(KeyCode.Alpha2) && !this.isReload) {    // 보조 무기 선택
            PlayerAnimatorController.instance.animator.SetTrigger("Holster_HG");

            yield return new WaitForSeconds(0.5f);

            this.playerAnimator.runtimeAnimatorController = this.playerHG;
            this.nextWeapon.SetActive(true);

            for (int i = 0; i < this.magazineList.Count; ++i) { // 다 끄기
                this.magazineList[i].SetActive(false);
            }

            gameObject.SetActive(false);
        }
    }
}
