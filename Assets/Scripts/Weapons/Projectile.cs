using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private AbilityEffect effect;
    [SerializeField] private GameObject destroyVFX;

    private Vector2 origin;
    private Animator animator;

    void Awake() {
        origin = transform.position;
	animator = GetComponent<Animator>();
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
            if (effect) {
                stats.ApplyEffect(effect);
            }
        }
        Explode();
    }

    void Explode() {
        // Play SFX
	GameObject vfx = Instantiate(
            destroyVFX,
            transform.position,
            Quaternion.identity
        );
	Destroy(vfx, 1);
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
