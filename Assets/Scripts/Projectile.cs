using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float range = 7f;
    [SerializeField] private float knockbackForce = 15f;
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

    // void OnTriggerEnter2D(Collider2D coll) {
    //     if(coll.tag == "Terrain") {
    //         return;
    //     }
    //     Health health = coll.gameObject.GetComponent<Health>();
    //     if(health != null) {
    //         health.DoDamage(damage);
    //     }
    //     Rigidbody2D rb = coll.gameObject.GetComponent<Rigidbody2D>();
    //     if(rb != null) {
    //         Vector2 direction = transform.position - coll.transform.position;
    //         rb.AddForce(direction.normalized * -knockbackForce, ForceMode2D.Impulse);
    //     }
    //     Explode();
    // }

    void OnCollisionEnter2D(Collision2D coll) {
        Health health = coll.gameObject.GetComponent<Health>();
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
