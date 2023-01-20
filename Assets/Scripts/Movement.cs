using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] private Transform orientation;
    [SerializeField] private CharacterController characterController;

    private Vector3 moveDir;
    private Vector3 moveForce;
    private float gravity = -200;


    public void UpdateMovement(float horizontal, float vertical, float moveSpeed) {
        this.moveDir = (vertical * this.orientation.forward + horizontal * this.orientation.right); // 이동 방향
        this.moveForce = new Vector3(this.moveDir.x * moveSpeed, this.moveDir.y, this.moveDir.z * moveSpeed); // 이동 힘


        this.characterController.Move(this.moveForce * Time.deltaTime);     // 이동

    }
}