using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float range = 7f;
    [SerializeField] private int damage;

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
        Health health = collision.gameObject.GetComponent<Health>();
        if(health != null) {
            health.DoDamage(damage);
        }
        Explode();
    }

    void Explode() {
        // Play VFX
        // Play SFX
        Destroy(gameObject);
    }

    public void SetDamage(int dmg) {
        this.damage = dmg;
    }

    public void SetRange(float range) {
        this.range = range;
    }
}
