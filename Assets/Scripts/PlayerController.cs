using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerAnimatorController))]
[RequireComponent(typeof(AudioController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    
    [Header("Component")]
    [SerializeField] private Transform orientation;
    [SerializeField] private CharacterController characterController;

    [Space(10f)]

    [Header("Charactor Movement")]
    [SerializeField] private float walkSpeed;           // 플레이어 걷기 속도
    [SerializeField] private float runSpeed;            // 플레이어 달리기 속도

    [Space(10f)]

    [SerializeField] private float rotateSpeedX = 5;    // 카메라 X축 회전 감도 (위/아래)
    [SerializeField] private float rotateSpeedY = 5;    // 카메라 Y축 회전 감도 (좌/우)

    [Space(10f)]

    [Header("Charactor Audio")]
    [SerializeField] private AudioClip audioWalking;
    [SerializeField] private AudioClip audioRunning;
    

    private AudioSource audioSource;
    private Vector3 moveDir;
    private Vector3 moveForce;
    private float gravity = -200;
    private float moveSpeed;           // 플레이어 이동 속도
    private float mouseX;
    private float mouseY;
    private float horizontal;
    private float vertical;
    private float run;
    private float _rotateSpeedX;
    private float _rotateSpeedY;
    private float rotateLimitMax = 80;  // 고개 들기 한계치 (카메라 위)
    private float rotateLimitMin = -80;   // 고개 숙이기 한계치 (카메라 아래)
    private float eulerAngleX;
    private float eulerAngleY;
    private bool moveFreeze;
        public bool MoveFreeze {
            get {
                return this.moveFreeze;
            }
            set {
                this.moveFreeze = value;
            }
        }
    private bool isRun;
        public bool IsRun {
            get {
                return this.isRun;
            }
            set {
                this.isRun = value;
            }
        }


    private void Init() {
        if (instance == null) {
            instance = this;
        }

        Cursor.visible = false;     // 마우스 커서 숨김
        Cursor.lockState = CursorLockMode.Locked;

        this.audioSource = gameObject.GetComponent<AudioSource>();
        
        this._rotateSpeedX = this.rotateSpeedX;
        this._rotateSpeedY = this.rotateSpeedY;
    }

    private void Awake() {
        Init();
    }

    private void Update() {
        UpdateRotate();     // 플레이어 시점
        UpdateMovement();   // 플레이어 이동
    }

    // 플레이어 시점 조작
    private void UpdateRotate() {
        this.mouseX = Input.GetAxis("Mouse X");
        this.mouseY = Input.GetAxis("Mouse Y");
        
        this.eulerAngleY += mouseX * rotateSpeedY;     // 마우스 좌/우 => X축   || 카메라 좌/우 => Y축
        this.eulerAngleX -= mouseY * rotateSpeedX;     // 마우스 위/아래 => Y축 || 카메라 위/아래 => X축

        this.eulerAngleX = Mathf.Clamp(this.eulerAngleX, this.rotateLimitMin, this.rotateLimitMax); // 고개 들기/숙이기 한계치 설정
        
        this.orientation.transform.rotation = Quaternion.Euler(0, this.eulerAngleY, 0); // 캐릭터 전진 방향 설정
        transform.rotation = Quaternion.Euler(this.eulerAngleX, this.eulerAngleY, 0);
    }

    // 플레이어 이동
    private void UpdateMovement() {
        if (this.moveFreeze) {
            return;
        }

        this.horizontal = Input.GetAxisRaw("Horizontal");
        this.vertical = Input.GetAxisRaw("Vertical");
        this.run = Input.GetAxisRaw("Run"); // Left Shift 키 입력

        if (this.run > 0 && this.vertical == 1) { // 달리기 상태일 경우,
            PlayerAnimatorController.instance.RunAnimationStatus();
            this.isRun = true;
            this.moveSpeed = this.runSpeed;

            if (!this.audioSource.isPlaying) {
                AudioController.instance.PlaySoundClip(this.audioSource, this.audioRunning);
            }
        }
        else if (this.run == 0 && this.vertical != 0 || this.horizontal != 0) {  // 걷기 상태일 경우,
            PlayerAnimatorController.instance.WalkAnimationStatus();
            this.isRun = false;
            this.moveSpeed = this.walkSpeed;

            if (!this.audioSource.isPlaying) {
                AudioController.instance.PlaySoundClip(this.audioSource, this.audioWalking);
            }
        }
        else {  // 대기 상태일 경우,
            this.audioSource.Stop();
            PlayerAnimatorController.instance.IdleAnimationStatus();
        }

        PlayerAnimatorController.instance.MovementAnimation(this.horizontal, this.vertical, this.isRun);

        this.moveDir = (this.vertical * this.orientation.forward + this.horizontal * this.orientation.right);    // 이동 방향 벡터
        this.moveForce = new Vector3(this.moveDir.x * this.moveSpeed, this.moveDir.y, this.moveDir.z * this.moveSpeed); // 이동 힘 벡터

        if (!this.characterController.isGrounded) {
            this.moveForce.y += this.gravity * Time.deltaTime;
        }

        this.characterController.Move(this.moveForce * Time.deltaTime); // 캐릭터 이동
    }

    // TODO: 플레이어 좌/우 기울이기

    
    // 플레이어 일시정지
    public void PlayerFreeze(bool isFreeze) {
        if (isFreeze) {
            this.rotateSpeedX = 0;
            this.rotateSpeedY = 0;
            this.moveFreeze = true;
        }
        else if (!isFreeze) {
            this.rotateSpeedX = _rotateSpeedX;
            this.rotateSpeedY = _rotateSpeedY;
            this.moveFreeze = false;
        }
    }
}