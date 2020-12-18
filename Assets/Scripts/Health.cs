using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] private int hitPoints = 100;

    void Update() {
        if(hitPoints <= 0) {
            Die();
        }
    }

    public void DoDamage(int amount) {
        hitPoints -= amount;
    }

    public void Restore(int amount) {
        hitPoints += amount;
    }

    private void Die() {
        // Play VFX
        // Play SFX
        Destroy(gameObject);
    }
}
