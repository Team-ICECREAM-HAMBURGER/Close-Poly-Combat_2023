using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour {
    [SerializeField] private GameObject destructibleObjectPieces;
    [SerializeField] private int currentHP;
    private bool isDestroyed = false;


    public void TakeDamage(int damage) {
        this.currentHP -= damage;

        if (this.currentHP <= 0 && this.isDestroyed == false) {
            this.isDestroyed = true;
            Instantiate(this.destructibleObjectPieces, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
            Destroy(gameObject);
        }
    }

}