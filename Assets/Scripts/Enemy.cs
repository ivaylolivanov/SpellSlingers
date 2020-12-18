using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hitPoints = 100;

    void Update() {
        if(hitPoints <= 0) {
            Die();
        }
    }

    void Die() {
        // Play VFX
        // Play SFX
        Destroy(gameObject);
    }
}
