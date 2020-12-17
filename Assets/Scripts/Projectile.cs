using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float range = 7f;

    private Vector2 origin;

    void Awake() {
        origin = transform.position;
    }

    void Update() {
        float currentDistance = Mathf.Abs(
            Vector2.Distance(transform.position, origin)
        );

        if(currentDistance > range) {
            Explode();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Explode();
    }

    void Explode() {
        // Play VFX
        // Play SFX
        Destroy(gameObject);
    }

    public void SetRange(float range) {
        this.range = range;
    }
}
