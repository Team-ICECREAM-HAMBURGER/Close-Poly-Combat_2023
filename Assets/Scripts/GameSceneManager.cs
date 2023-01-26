using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {
    private bool isPause;
    [SerializeField] private GameObject Panel_GamePause;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;


    private void Init() {
        this.restartButton.onClick.AddListener(() => OnRestart());
        this.exitButton.onClick.AddListener(() => OnExit());
    }

    private void Awake() {
        Init();
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (this.isPause) {
                OnResume();
            }
            else {
                OnPause();
            }
        }
    }

    private void OnResume() {
        Time.timeScale = 1;
        this.isPause = false;
        Cursor.visible = false;     // 마우스 커서 숨김
        Cursor.lockState = CursorLockMode.Locked;
        this.Panel_GamePause.SetActive(false);
    }

    private void OnPause() {
        Time.timeScale = 0;
        this.isPause = true;
        Cursor.visible = true;     // 마우스 커서 숨김 해제
        Cursor.lockState = CursorLockMode.None;
        this.Panel_GamePause.SetActive(true);
    }

    private void OnRestart() {
        OnResume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnExit() {
        Application.Quit();
    }
}
