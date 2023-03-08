using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSG : WeaponController {
    private void Init() {
        base.Init();
        base.ReloadTime = 3.18f;
    }

    private void Awake() {
        Init();
    }

    private void OnEnable() {
        WeaponUIController.instance.onAmmoEvent.Invoke(this.weaponSetting.currentAmmo, this.weaponSetting.maxAmmo);    // 탄 수 UI Invoke
        WeaponUIController.instance.onMagzineEvent.Invoke(base.weaponSetting.currentMagazine);
    }

    private void Update() {
        base.UpdateFire();
        UpdateAim();
        base.UpdateReload();
    }

    public override IEnumerator OnReload() {
        base.IsReload = true;
        PlayerAnimatorController.instance.IsReload = true;   // Animation(Reload) Play
        base.WeaponAnimator.SetTrigger("Reloading");
        
        while (base.weaponSetting.currentAmmo < base.weaponSetting.maxAmmo) {
            AudioController.instance.PlaySoundOneShot(base.AudioSource, base.audioReload); // 탄창 수 UI Invoke

            yield return new WaitForSeconds(0.767f);
            
            base.weaponSetting.currentAmmo += 1;
            
            WeaponUIController.instance.onAmmoEvent.Invoke(base.weaponSetting.currentAmmo, base.weaponSetting.maxAmmo);    // 탄 수 UI Invoke
        }

        base.IsReload = false;
        PlayerAnimatorController.instance.IsReload = false;
        base.weaponSetting.currentAmmo = base.weaponSetting.maxAmmo;
        base.weaponSetting.currentMagazine -= 1;
        WeaponUIController.instance.onMagzineEvent.Invoke(base.weaponSetting.currentMagazine);  

        yield return null;
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
