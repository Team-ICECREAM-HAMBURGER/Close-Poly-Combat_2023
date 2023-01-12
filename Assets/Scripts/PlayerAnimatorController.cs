using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour {
    public static PlayerAnimatorController instance;
    [HideInInspector] public Animator animator;
    public float MoveSpeed {
        set => this.animator.SetFloat("Move Speed", value, 0.05f, Time.deltaTime);
        get => this.animator.GetFloat("Move Speed");
    }
    public float Aiming {
        set => this.animator.SetFloat("Aiming", value, 0.05f, Time.deltaTime);
        get => this.animator.GetFloat("Aiming");
    }
    public bool IsAim {
        set => this.animator.SetBool("Aim", value);
        get => this.animator.GetBool("Aim");
    }
    public bool IsRun {
        set => this.animator.SetBool("Run", value);
        get => this.animator.GetBool("Run");
    }
    public bool IsWalk {
        set => this.animator.SetBool("Walk", value);
        get => this.animator.GetBool("Walk");
    }
    public bool IsReload {
        set => this.animator.SetBool("Reload", value);
        get => this.animator.GetBool("Reload");
    }


    private void Init() {
        if (instance == null) {
            instance = this;
        }
        
        this.animator = gameObject.GetComponent<Animator>();
    }

    private void Awake() {
        Init();
    }

    public void MovementAnimation(float horizontal, float vertical, bool isRun = false) {
        if (horizontal != 0 || vertical != 0) {   // 이동 중일 때
            this.MoveSpeed = isRun == true ? 1 : 0.5f;
        }
        else {  // 정지 상태일 때
            this.MoveSpeed = 0;
        }
    }
}