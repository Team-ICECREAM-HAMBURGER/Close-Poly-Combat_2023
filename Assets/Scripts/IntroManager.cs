using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour {
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;


    private void Init() {
        this.startButton.onClick.AddListener(() => OnPlay());
        this.exitButton.onClick.AddListener(() => OnExit());
    }

    private void Awake() {
        Init();
    }

    private void OnPlay() {
        SceneLoader.instance.LoadScene("SampleScene");
    }

    private void OnExit() {
        Application.Quit();
    }

}