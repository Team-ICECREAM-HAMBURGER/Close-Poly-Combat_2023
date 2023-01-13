using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class AmmoEvent : UnityEngine.Events.UnityEvent<int, int> { }
[System.Serializable]
public class MagazineEvent : UnityEngine.Events.UnityEvent<int> { }

public class WeaponAR : MonoBehaviour {
    [SerializeField] private WeaponSetting weaponSetting;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private AudioClip[] audios;
    private AudioSource audioSource;
    private Animator animator;
    private Camera mainCamera;
    private ImpactMemoryPool impactMemoryPool;
    private float lastAttackTime;
    private bool isReload;
    private bool isAimSoundPlay;

    [HideInInspector] public AmmoEvent onAmmoEvent = new AmmoEvent();
    [HideInInspector] public MagazineEvent onMagzineEvent = new MagazineEvent();


    private void Init() {
        this.weaponSetting.currentAmmo = this.weaponSetting.maxAmmo;
        this.weaponSetting.currentMagazine = this.weaponSetting.maxMagazine;
        this.lastAttackTime = 0;
        this.weaponSetting.weaponType = WeaponType.AR;
        this.animator = gameObject.GetComponent<Animator>();
        this.mainCamera = Camera.main;
        this.impactMemoryPool = gameObject.GetComponent<ImpactMemoryPool>();
        this.audioSource = gameObject.GetComponent<AudioSource>();
        this.isAimSoundPlay = false;
    }

    private void Awake() {
        Init();
    }

    private void Update() {
        UpdateFire();
        UpdateAim();
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
            AudioController.instance.PlaySoundOneShot(this.audioSource, this.audios[1]);
            TwoStepRayCast();
        }

        yield return null; 
    }

    private IEnumerator OnReload() {
        this.isReload = true;
        PlayerAnimatorController.instance.IsReload = true;   // Animation(Reload) Play
        this.animator.SetTrigger("Reloading");
        AudioController.instance.PlaySoundOneShot(this.audioSource, this.audios[2]);
            
        yield return new WaitForSeconds(3.18f);
            
        this.isReload = false;
        PlayerAnimatorController.instance.IsReload = false;
        this.weaponSetting.currentAmmo = this.weaponSetting.maxAmmo;
        this.weaponSetting.currentMagazine -= 1;

        yield return null;
    }

    private void TwoStepRayCast() {
        Ray ray;
        RaycastHit hit;
        Vector3 targetPoint = Vector3.zero;
        
        ray = this.mainCamera.ViewportPointToRay(Vector2.one * 0.5f);   // 화면 중앙 지점으로 Ray
        
        if (Physics.Raycast(ray, out hit, this.weaponSetting.attackDistance)) {     // 무기 사정거리만큼 Ray 발사 (화면 정중앙)
            targetPoint = hit.point;
        }
        else {
            targetPoint = ray.origin + ray.direction * this.weaponSetting.attackDistance;   // hit이 null일 경우, Ray를 무기 사정거리까지 쭉 발사
        }

        Vector3 attackDirection = (targetPoint - this.bulletSpawnPoint.position).normalized;    // (화면 정중앙 - 총구 위치).일반화

        if (Physics.Raycast(bulletSpawnPoint.position, attackDirection, out hit, this.weaponSetting.attackDistance)) {
            this.impactMemoryPool.SpawnImpact(hit); // Hit Impact
            Debug.Log("Hit: " + hit.transform.name);
        }
    }

    private void UpdateAim() {
        if (Input.GetMouseButton(1) && !PlayerController.instance.isRun) {  // 마우스 우클릭 (유지)
            if (!this.isAimSoundPlay) {
                this.isAimSoundPlay = true;
                AudioController.instance.PlaySoundOneShot(this.audioSource, this.audios[0]);  // Aim In
            }

            PlayerAnimatorController.instance.IsAim = true; // 정조준 모드 활성화
        }
        else if (Input.GetMouseButtonUp(1)) {
            this.isAimSoundPlay = false;
            PlayerAnimatorController.instance.IsAim = false;
        }

        if (PlayerAnimatorController.instance.IsAim) {
            PlayerAnimatorController.instance.Aiming = 1;   // Blend Tree
        }
        else if (!PlayerAnimatorController.instance.IsAim) {
            PlayerAnimatorController.instance.Aiming = 0;   // Blend Tree
        }
    }
}
