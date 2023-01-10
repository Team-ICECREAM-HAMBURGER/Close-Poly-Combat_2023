using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAR : MonoBehaviour {
    [SerializeField] private WeaponSetting weaponSetting;
    private float lastAttackTime;
    private bool isReload;
    private Animator animator;


    private void Init() {
        this.weaponSetting.currentAmmo = this.weaponSetting.maxAmmo;
        this.weaponSetting.currentMagazine = this.weaponSetting.maxMagazine;
        this.lastAttackTime = 0;
        this.weaponSetting.weaponType = WeaponType.AR;
        this.animator = gameObject.GetComponent<Animator>();
    }

    private void Awake() {
        Init();
    }

    private void Update() {
        UpdateFire();
        UpdateReload();
    }

    private void UpdateFire() {
        if (this.weaponSetting.isAuto) {
            if (this.isReload) {    // 재장전 중에는 사격 불가
                return;
            }
            
            if (Input.GetMouseButton(0)) {    // 조정간 자동
                OnFire();
            }
        }
        else if (this.weaponSetting.isSemi) {
            if (this.isReload) {    // 재장전 중에는 사격 불가
                return;
            }

            if (Input.GetMouseButton(0)) {    // 조정간 반자동
                OnFire();
            }
        }
    }

    private void UpdateReload() {
        if (Input.GetKeyDown(KeyCode.R)) {
            OnReload();
        }
    }

    private void OnFire() {
        if (Time.time - this.lastAttackTime > this.weaponSetting.attackRate) {
            if (PlayerController.instance.isRun) {  // 달리기 도중에는 사격 불가
                return;
            }
            
            if (this.weaponSetting.currentAmmo <= 0) {  // 탄약이 고갈되면 사격 불가
                return;
            }

            this.lastAttackTime = Time.time;
            this.weaponSetting.currentAmmo -= 1;
            // onAmmoEvent.Invoke() 

            PlayerAnimatorController.instance.animator.Play("Fire", 1, 0);  // Animation(Fire) Play
        }
    }

    private void OnReload() {
        PlayerAnimatorController.instance.animator.SetTrigger("Reloading");   // Animation(Reload) Play
        this.weaponSetting.currentAmmo = this.weaponSetting.maxAmmo;
    }
}
