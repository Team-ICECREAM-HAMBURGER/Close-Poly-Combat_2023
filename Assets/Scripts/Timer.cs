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
    private float startTime;

    private void Init() {
        if (instance == null) {
            instance = this;
        }
        this.timerText.text = "00.00";
        
    }

    private void Awake() {
        Init();
    }

    private void OnEnable() {
        this.startTime = Time.time;
        StartCoroutine(OnTimer());    
    }


    private IEnumerator OnTimer() {
        while(this.targetCount > 0) {
            this.timerText.text = $"{Time.time - this.startTime:N2}";
            this.time = this.timerText.text;
            yield return new WaitForSeconds(0.01f);
        }
        
        this.timerText.text = this.time;
    }
}
