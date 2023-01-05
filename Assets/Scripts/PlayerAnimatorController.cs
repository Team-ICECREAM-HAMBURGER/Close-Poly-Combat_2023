using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour {
    private Animator animator;
    public float MoveSpeed {
        set => this.animator.SetFloat("Move Speed", value, 0.05f, Time.deltaTime);
        get => this.animator.GetFloat("Move Speed");
    }


    private void Init() {
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