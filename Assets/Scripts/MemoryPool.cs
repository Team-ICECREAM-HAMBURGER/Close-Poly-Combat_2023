using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPool : MonoBehaviour {
    private class PoolItem {
        public bool isActive;
        public GameObject gameObject;
    }


    private int increaseCount = 5;
    private int maxCount;
    private int activeCount;
    private GameObject poolObject;
    private List<PoolItem> poolItemList;


    public MemoryPool(GameObject poolObject) {
        this.maxCount = 0;
        this.activeCount = 0;
        this.poolObject = poolObject;
        this.poolItemList = new List<PoolItem>();

        InstantiateObjects();
    }

    private void InstantiateObjects() {
        this.maxCount += this.increaseCount;

        for (int i = 0; i < this.increaseCount; ++i) {
            PoolItem poolItem = new PoolItem();

            poolItem.isActive = false;
            poolItem.gameObject = GameObject.Instantiate(this.poolObject);
            poolItem.gameObject.SetActive(poolItem.isActive);

            this.poolItemList.Add(poolItem);
        }
    }

    private void DestroyObjects() {
        if (this.poolItemList == null) {
            return;
        }

        int count = this.poolItemList.Count;

        for (int i = 0; i < count; ++i) {
            GameObject.Destroy(this.poolItemList[i].gameObject);
        }

        this.poolItemList.Clear();
    }

    public GameObject ActivateObjects() {
        Debug.Log(this.maxCount + " " + this.activeCount);
        if (this.poolItemList == null) {
            return null;
        }

        if (this.maxCount == this.activeCount) {
            InstantiateObjects();
        }

        int count = this.poolItemList.Count;

        for (int i = 0; i < count; ++i) {
            PoolItem poolItem = this.poolItemList[i];
            
            if (!poolItem.isActive) {
                this.activeCount++;
                poolItem.isActive = true;
                poolItem.gameObject.SetActive(poolItem.isActive);

                return poolItem.gameObject;
            }
        }

        return null;
    }

    public void DeActiveObjects(GameObject deactiveObject) {
        if (this.poolItemList == null || deactiveObject == null) {
            return;
        }

        int count = this.poolItemList.Count;
        
        for (int i = 0; i < count; ++i) {
            PoolItem poolItem = this.poolItemList[i];

            if (poolItem.gameObject == deactiveObject) {
                poolItem.isActive = false;
                poolItem.gameObject.SetActive(poolItem.isActive);
                
                return;
            }
        }
    }
}