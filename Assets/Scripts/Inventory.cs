using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponIndx {
    AR = 0,
    HG = 1,
    SG = 2
}


public class Inventory : MonoBehaviour {
    [Header("Component")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private List<GameObject> weapons;
    [SerializeField] private List<AnimatorOverrideController> playerOverrideControllers;

    private int indx;

    private void Init() {
        this.indx = 0;
    }

    private void Awake() {
        Init();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {         // AR
            this.indx = (int)WeaponIndx.AR;
            StartCoroutine("UpdateChangeWeapon");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {    // HG
            this.indx = (int)WeaponIndx.HG;
            StartCoroutine("UpdateChangeWeapon");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {    // SG
            this.indx = (int)WeaponIndx.SG;
            StartCoroutine("UpdateChangeWeapon");
        }
    }

    private IEnumerator UpdateChangeWeapon() {
        if (!PlayerAnimatorController.instance.IsReload) {
            PlayerAnimatorController.instance.animator.SetTrigger("Holster");

            yield return new WaitForSeconds(0.5f);

            this.playerAnimator.runtimeAnimatorController = this.playerOverrideControllers[this.indx];  // Animator Controller 변경; PlayerAR

            foreach(GameObject weapon in this.weapons) {
                weapon.SetActive(false);    // 다 끄기
            }

            this.weapons[this.indx].SetActive(true);    // AR 켜기
        }
    }
}