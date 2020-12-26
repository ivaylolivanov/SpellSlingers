using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {
    [SerializeField] private int damage = 1;
    [SerializeField] private float timeBetweenDamage = 2f;

    private bool waiting = false;
    void OnTriggerStay2D(Collider2D coll) {
        if(! waiting) {
            waiting = true;
            StartCoroutine(Damage(coll));
        }
    }

    private IEnumerator Damage(Collider2D coll) {
        Health healthComponent = coll.GetComponent<Health>();
        if(healthComponent != null) {
            healthComponent.DoDamage(damage);
            yield return new WaitForSeconds(timeBetweenDamage);
            waiting = false;
        }
    }
}
