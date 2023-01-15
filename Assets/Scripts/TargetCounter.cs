using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetCounter : MonoBehaviour {
    [SerializeField] private TMP_Text targetText;

    
    private void Update() {
        UpdateTargetCount();
    }

    private void UpdateTargetCount() {
        this.targetText.text = "Remaining Target: " + Timer.instance.TargetCount;
    }
}