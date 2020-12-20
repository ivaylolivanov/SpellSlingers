using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    [SerializeField] private int maxHitPoints = 100;

    private int currentHitPoints;

    void Start() {
        currentHitPoints = maxHitPoints;
    }

    void Update() {
        if(currentHitPoints <= 0) {
            Die();
        }
    }

    public void DoDamage(int amount) {
        currentHitPoints -= amount;
    }

    public void Restore(int amount) {
        currentHitPoints += amount;
        if (currentHitPoints >= maxHitPoints) {
            currentHitPoints = maxHitPoints;
        }
    }

    private void Die() {
        // Play VFX
        // Play SFX
        Destroy(gameObject);
    }
}
