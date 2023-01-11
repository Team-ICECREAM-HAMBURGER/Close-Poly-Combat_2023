using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour {
    private ParticleSystem[] particles;
    private MemoryPool memoryPool;


    private void Init() {
        this.particles = gameObject.GetComponentsInChildren<ParticleSystem>();
    }

    public void Setup(MemoryPool memoryPool) {
        this.memoryPool = memoryPool;
    } 

    private void Awake() {
        Init();
    }

    private void Update() {
        foreach(ParticleSystem particle in this.particles) {
            if (!particle.isPlaying) {
                this.memoryPool.DeActiveObjects(gameObject);
            }
        }    
    }

}