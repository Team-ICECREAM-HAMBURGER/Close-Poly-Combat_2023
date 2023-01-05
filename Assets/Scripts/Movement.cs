using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] private Transform orientation;
    [SerializeField] private CharacterController characterController;
    [SerializeField] float moveSpeed;
    private Vector3 moveDir;
    private Vector3 moveForce;


    public void UpdateMovement(float horizontal, float vertical) {
        this.moveDir = (vertical * this.orientation.forward + horizontal * this.orientation.right); // 이동 방향
        this.moveForce = new Vector3(this.moveDir.x * this.moveSpeed, this.moveDir.y, this.moveDir.z * this.moveSpeed); // 이동 힘

        this.characterController.Move(this.moveForce * Time.deltaTime);     // 이동
    }
}