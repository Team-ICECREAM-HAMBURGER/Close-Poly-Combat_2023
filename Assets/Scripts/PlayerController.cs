using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(RotateToMouse))]
public class PlayerController : MonoBehaviour {
    private RotateToMouse rotateToMouse;
    private float mouseX;
    private float mouseY;


    private void Init() {
        Cursor.visible = false;     // 마우스 커서 숨김
        Cursor.lockState = CursorLockMode.Locked;
        this.rotateToMouse = gameObject.GetComponent<RotateToMouse>();

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
        
    }




}