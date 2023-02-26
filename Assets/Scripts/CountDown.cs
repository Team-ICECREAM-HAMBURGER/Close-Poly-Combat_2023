using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour {
    [SerializeField] private TMP_Text countText;

    int countNum = 3;

    private void Start() {
        this.countText.text = this.countNum.ToString();
        StartCoroutine("Counter");
    }

    private IEnumerator Counter() {
        PlayerController.instance.PlayerFreeze(true);
        
        while(this.countNum > 0) {
            yield return new WaitForSeconds(1);
            this.countNum -= 1;
            this.countText.text = this.countNum.ToString();
        }

        PlayerController.instance.PlayerFreeze(false);
        Timer.instance.TimerOn();
        gameObject.SetActive(false);
        yield return null;
    }
}