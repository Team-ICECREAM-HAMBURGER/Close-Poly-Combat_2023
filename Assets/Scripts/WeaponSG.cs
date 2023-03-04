using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSG : WeaponController {
    private void Init() {
        base.Init();
        base.ReloadTime = 3.18f;
        base.ikHandL.weight = 1;
        base.ikHandL.data.target = base.refIKHandL;
    }

    private void OnEnable() {
        Init();
    }

    private void Update() {
        base.UpdateFire();
        base.UpdateAim();
        base.UpdateReload();
    }

    public override IEnumerator OnReload() {
        base.ikHandL.weight = 0;
        
        while (base.weaponSetting.currentAmmo < base.weaponSetting.maxAmmo) {
            base.IsReload = true;
            PlayerAnimatorController.instance.IsReload = true;   // Animation(Reload) Play
            base.WeaponAnimator.SetTrigger("Reloading");
            AudioController.instance.PlaySoundOneShot(base.AudioSource, base.audioReload); // 탄창 수 UI Invoke

            yield return new WaitForSeconds(1f);

            base.weaponSetting.currentAmmo += 1;
            WeaponUIController.instance.onAmmoEvent.Invoke(base.weaponSetting.currentAmmo, base.weaponSetting.maxAmmo);    // 탄 수 UI Invoke
            WeaponUIController.instance.onMagzineEvent.Invoke(base.weaponSetting.currentMagazine);  
        }

        base.IsReload = false;
        PlayerAnimatorController.instance.IsReload = false;
        base.weaponSetting.currentAmmo = base.weaponSetting.maxAmmo;
        base.weaponSetting.currentMagazine -= 1;


        base.ikHandL.weight = 1;

        yield return null;

    }




}
