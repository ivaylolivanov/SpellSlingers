using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {
    [SerializeField] private int damage = 1;
    [SerializeField] private float timeBetweenDamage = 2f;

    private bool waiting = false;
    private HashSet<ObjectStats> targets;

    void Start() {
        targets = new HashSet<ObjectStats>();
    }

    void OnTriggerEnter2D(Collider2D target) {
        ObjectStats targetStats = target.GetComponent<ObjectStats>();
        if(targetStats) {
            targets.Add(targetStats);
        }
    }

    void Update() {
        if(! waiting) {
            waiting = true;
            StartCoroutine(DamageTargets());
        }
    }

    void OnTriggerExit2D(Collider2D target) {
        ObjectStats targetStats = target.GetComponent<ObjectStats>();
        if(targetStats) {
            targets.Remove(targetStats);
        }
    }

    private IEnumerator DamageTargets() {
        foreach(ObjectStats targetStats in targets) {
            targetStats.TakeDamage(damage);
        }
        yield return new WaitForSeconds(timeBetweenDamage);
        waiting = false;
    }
}
