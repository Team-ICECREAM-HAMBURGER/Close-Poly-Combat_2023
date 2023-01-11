using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAR : MonoBehaviour {
    [SerializeField] private WeaponSetting weaponSetting;
    [SerializeField] private ParticleSystem muzzleFlash;
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
            if (Input.GetMouseButton(0)) {    // 조정간 자동
                if (this.isReload) {    // 재장전 중에는 사격 불가
                    return;
                }

                if (PlayerController.instance.isRun) {  // 달리기 도중에는 사격 불가
                    return;
                }

                if (this.weaponSetting.currentAmmo <= 0) {  // 탄약이 고갈되면 사격 불가
                    return;
                }

                StartCoroutine("OnFire");
            }
        }
        else if (this.weaponSetting.isSemi) {
            if (Input.GetMouseButtonDown(0)) {    // 조정간 반자동
                if (this.isReload) {    // 재장전 중에는 사격 불가
                    return;
                }

                if (PlayerController.instance.isRun) {  // 달리기 도중에는 사격 불가
                    return;
                }

                if (this.weaponSetting.currentAmmo <= 0) {  // 탄약이 고갈되면 사격 불가
                    return;
                }

                StartCoroutine("OnFire");
            }
        }
    }

    private void UpdateReload() {
        if (Input.GetKeyDown(KeyCode.R) && this.weaponSetting.currentAmmo < this.weaponSetting.maxAmmo && this.weaponSetting.currentMagazine > 0) {
            if (PlayerController.instance.isRun) {  // 달리기 도중에는 재장전 불가
                return;
            }

            StartCoroutine("OnReload");
        }
        else if (this.weaponSetting.currentAmmo <= 0 && !this.isReload && this.weaponSetting.currentMagazine > 0) {
            StartCoroutine("OnReload");
        }
    }

    private IEnumerator OnFire() {
        if (Time.time - this.lastAttackTime > this.weaponSetting.attackRate) {
            this.lastAttackTime = Time.time;
            this.weaponSetting.currentAmmo -= 1;
            // onAmmoEvent.Invoke()
            this.muzzleFlash.Play();
            PlayerAnimatorController.instance.animator.Play("Fire", 1, 0);  // Animation(Fire) Play
        }

        yield return null; 
    }

    private IEnumerator OnReload() {
        this.isReload = true;
        PlayerAnimatorController.instance.animator.SetTrigger("Reloading");   // Animation(Reload) Play
        this.animator.SetTrigger("Reloading");
            
        yield return new WaitForSeconds(3.18f);
            
        this.isReload = false;
        this.weaponSetting.currentAmmo = this.weaponSetting.maxAmmo;
        this.weaponSetting.currentMagazine -= 1;

        yield return null;
    }
}
