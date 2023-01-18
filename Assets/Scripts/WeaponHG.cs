using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHG : MonoBehaviour {
    [SerializeField] private Animator animator;
    

    public void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            PlayerAnimatorController.instance.IsReload = true;   // Animation(Reload) Play
            this.animator.SetTrigger("Reloading");
        }
    }
}