using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasingMemoryPool : MonoBehaviour {
    [SerializeField] private GameObject casingPrefab;
    private MemoryPool memoryPool;


    private void Init() {
        this.memoryPool = new MemoryPool(this.casingPrefab);
    }

    private void Awake() {
        Init();
    }

    public void SpawnCasing(Vector3 position, Vector3 direction) {
        GameObject item = this.memoryPool.ActivateObjects();
        item.transform.position = position;
        item.transform.rotation = Random.rotation;
        item.GetComponent<Casing>().Init(this.memoryPool, direction);
    }
}