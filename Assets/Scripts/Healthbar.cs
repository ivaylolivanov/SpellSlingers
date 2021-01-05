using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
    private Slider healthBarSlider;

    void Awake() {
        healthBarSlider = GetComponent<Slider>();
    }

    public void Initialize(int maxHealth) {
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = maxHealth;
    }

    public void UpdateHealthBar(int currentHealth) {
        healthBarSlider.value = currentHealth;
    }

}
