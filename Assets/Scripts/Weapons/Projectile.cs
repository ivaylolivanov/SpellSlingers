using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private AbilityEffect effect;

    private Vector2 origin;

    void Awake() {
        origin = transform.position;
    }

    void Update() {
        float currentDistance = (
            (Vector2)transform.position - origin
        ).sqrMagnitude;

        if(currentDistance > range * range) {
            Explode();
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        ObjectStats stats = coll.gameObject.GetComponent<ObjectStats>();
        if(stats) {
            stats.TakeDamage(damage);
            stats.ApplyEffect(effect);
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

    public void SetEffect(AbilityEffect effect) {
        this.effect = effect;
    }
}
