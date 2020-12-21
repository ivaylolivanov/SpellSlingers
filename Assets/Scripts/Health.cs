using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    [SerializeField] private int maxHitPoints = 100;
    [SerializeField] private GameObject healthBar = null;

    private int currentHitPoints;
    private Slider healthBarSlider;

    void Start() {
        currentHitPoints = maxHitPoints;
        if(healthBar != null) {
            healthBarSlider = healthBar.GetComponent<Slider>();
            healthBarSlider.maxValue = maxHitPoints;
            healthBarSlider.value = currentHitPoints;
        }
    }

    void Update() {
        if(currentHitPoints <= 0) {
            Die();
        }
    }

    public void DoDamage(int amount) {
        currentHitPoints -= amount;
        if(healthBar != null) {
            healthBarSlider.value = currentHitPoints;
        }
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
