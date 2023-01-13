using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public static AudioController instance;


    private void Init() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Awake() {
        Init();
    }

    public void PlaySoundOneShot(AudioSource audioSource, AudioClip clip) {
        audioSource.PlayOneShot(clip);        
    }

    public void PlaySound(AudioSource audioSource, AudioClip clip) {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}