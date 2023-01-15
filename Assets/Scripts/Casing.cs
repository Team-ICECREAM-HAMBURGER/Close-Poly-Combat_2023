using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casing : MonoBehaviour {
    [SerializeField] private AudioClip[] audioClips;
    private Rigidbody rigidbody;
    private AudioSource audioSource;
    private MemoryPool memoryPool;
    private float deactivateTime = 5.0f;
    

    public void Init(MemoryPool memoryPool, Vector3 direction) {
        this.rigidbody = gameObject.GetComponent<Rigidbody>();
        this.audioSource = gameObject.GetComponent<AudioSource>();
        this.memoryPool = memoryPool;

        this.rigidbody.AddForce(new Vector3(direction.x, 1.0f, direction.z), ForceMode.VelocityChange);
        this.rigidbody.angularVelocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        StartCoroutine("DeactivateAfterTime");
    }

    private void OnCollisionEnter(Collision other) {
        int index = Random.Range(0, this.audioClips.Length);
        
        this.audioSource.clip = this.audioClips[index];
        this.audioSource.Play();
    }

    private IEnumerator DeactivateAfterTime() {
        yield return new WaitForSeconds(this.deactivateTime);

        this.memoryPool.DeActiveObjects(this.gameObject);
    }
}
