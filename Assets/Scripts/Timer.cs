using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {
    public static Timer instance;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private int targetCount;
        public int TargetCount {
            set {
                this.targetCount -= value;
            }
            get {
                return this.targetCount;
            }
        }
    private string time;


    private void Init() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Awake() {
        Init();
    }

    private void Update() {
        UpdateTimer();
    }

    public void UpdateTimer() {
        if (this.targetCount > 0) {
            this.timerText.text = $"{Time.time:N2}";
            this.time = this.timerText.text;
        }
        else {
            this.timerText.text = this.time;
        }
    }
}
