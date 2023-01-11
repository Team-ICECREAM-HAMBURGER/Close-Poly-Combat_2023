using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ImpactType {
    Normal = 0,
    Target = 1
}

public class ImpactMemoryPool : MonoBehaviour {
    [SerializeField] private GameObject[] impactPrefabs;
    [SerializeField] private MemoryPool[] memoryPools;
    private GameObject impact;


    private void Init() {
        this.memoryPools = new MemoryPool[this.impactPrefabs.Length];

        for (int i = 0; i < this.impactPrefabs.Length; ++i) {
            this.memoryPools[i] = new MemoryPool(this.impactPrefabs[i]);
        }
    }

    private void Awake() {
        Init();
    }

    public void SpawnImpact(RaycastHit hit) {
        Debug.Log(hit.transform.tag + " " + hit.transform.name);
        
        if (hit.transform.CompareTag("Concrete")) {
            this.impact = this.memoryPools[(int)ImpactType.Normal].ActivateObjects();
            this.impact.transform.position = hit.point;
            this.impact.transform.rotation = Quaternion.LookRotation(hit.normal);

            this.impact.GetComponent<Impact>().Setup(this.memoryPools[(int)ImpactType.Normal]);
        }
        else if (hit.transform.CompareTag("Target")) {

        }
    }
}