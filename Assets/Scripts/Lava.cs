using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {
    [SerializeField] private int damage = 1;
    [SerializeField] private float timeBetweenDamage = 2f;

    private bool waiting = false;
    private HashSet<Health> targets;

    void Start() {
        targets = new HashSet<Health>();
    }

    void OnTriggerEnter2D(Collider2D target) {
        Health targetHealth = target.GetComponent<Health>();
        if(targetHealth) {
            targets.Add(targetHealth);
        }
    }

    void Update() {
        if(! waiting) {
            waiting = true;
            StartCoroutine(DamageTargets());
        }
    }

    void OnTriggerExit2D(Collider2D target) {
        Health targetHealth = target.GetComponent<Health>();
        if(targetHealth) {
            targets.Remove(targetHealth);
        }
    }

    private IEnumerator DamageTargets() {
        foreach(Health target in targets) {
            target.DoDamage(damage);
        }
        yield return new WaitForSeconds(timeBetweenDamage);
        waiting = false;
    }
}
