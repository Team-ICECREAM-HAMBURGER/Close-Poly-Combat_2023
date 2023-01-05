using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour {
    [SerializeField] private float rotateSpeedX = 5;    // 카메라 X축 회전 감도 (위/아래)
    [SerializeField] private float rotateSpeedY = 5;    // 카메라 Y축 회전 감도 (좌/우)
    [SerializeField] private Transform orientation;
    private float rotateLimitMax = 80;  // 고개 들기 한계치 (카메라 위)
    private float rotateLimitMin = -80;   // 고개 숙이기 한계치 (카메라 아래)
    private float eulerAngleX;
    private float eulerAngleY;


    public void UpdateRotate(float mouseX, float mouseY) {
        this.eulerAngleY += mouseX * this.rotateSpeedY;     // 마우스 좌/우 => X축   || 카메라 좌/우 => Y축
        this.eulerAngleX -= mouseY * this.rotateSpeedX;     // 마우스 위/아래 => Y축 || 카메라 위/아래 => X축

        this.eulerAngleX = Mathf.Clamp(this.eulerAngleX, this.rotateLimitMin, this.rotateLimitMax); // 고개 들기/숙이기 한계치 설정
        
        this.orientation.transform.rotation = Quaternion.Euler(0, this.eulerAngleY, 0); // 캐릭터 전진 방향 설정
        transform.rotation = Quaternion.Euler(this.eulerAngleX, this.eulerAngleY, 0);
    }
}