using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(RotateToMouse))]
[RequireComponent (typeof(Movement))]
public class PlayerController : MonoBehaviour {
    private RotateToMouse rotateToMouse;
    private PlayerAnimatorController playerAnimatorController;
    private Movement movement;
    private float mouseX;
    private float mouseY;
    private float horizontal;
    private float vertical;
    private float run;
    private bool isRun;


    private void Init() {
        Cursor.visible = false;     // 마우스 커서 숨김
        Cursor.lockState = CursorLockMode.Locked;

        this.rotateToMouse = gameObject.GetComponent<RotateToMouse>();
        this.movement = gameObject.GetComponent<Movement>();
        this.playerAnimatorController = gameObject.GetComponent<PlayerAnimatorController>();
    }

    private void Awake() {
        Init();
    }

    private void Update() {
        UpdateRotate();     // 플레이어 시점
        UpdateMovement();   // 플레이어 이동
    }

    // 플레이어 시점
    private void UpdateRotate() {
        this.mouseX = Input.GetAxis("Mouse X");
        this.mouseY = Input.GetAxis("Mouse Y");

        this.rotateToMouse.UpdateRotate(this.mouseX, this.mouseY);  // 마우스로 시점 조작
    }

    // 플레이어 이동
    private void UpdateMovement() {
        this.horizontal = Input.GetAxisRaw("Horizontal");
        this.vertical = Input.GetAxisRaw("Vertical");
        this.run = Input.GetAxisRaw("Run");

        if (this.run > 0) { // 달리기 상태일 경우,
            this.isRun = true;
        }
        else {
            this.isRun = false;
        }

        this.playerAnimatorController.MovementAnimation(this.horizontal, this.vertical, this.isRun);
        this.movement.UpdateMovement(this.horizontal, this.vertical);
    }


}