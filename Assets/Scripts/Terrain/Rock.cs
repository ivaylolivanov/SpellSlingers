using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : SpawnableObject {
    [SerializeField] private LayerMask pushIgnoreLayer;

    [SerializeField] private float range;
    [SerializeField] private float force;

    private Animator animator;
    private int pushIgnoreMask = 0;
    void Start() {
	animator = GetComponent<Animator>();
	pushIgnoreMask = ~ pushIgnoreLayer.value;
	Push(transform.position);
    }

    public override void SetForce(float force) {
	this.force = force;
    }

    public override void SetRange(float range) {
        this.range = range;
    }

    public override void Die(float delay = 0) {
        StartCoroutine(DieWithAnimation(delay));
    }

    private IEnumerator DieWithAnimation(float delay) {
        yield return new WaitForSeconds(delay);

	animator.SetBool("destroy", true);
	Destroy(gameObject, 1);
    }

    private void Push(Vector2 location) {
    	Collider2D[] targets = Physics2D.OverlapCircleAll(location, range, pushIgnoreMask);

    	foreach(Collider2D target in targets) {
	    if(
               GameObject.ReferenceEquals(
                   target.transform.gameObject,
                   transform.gameObject)
            ) {
        	continue;
	    }

            Vector2 direction = target.gameObject.transform.position - transform.position;
    	    Rigidbody2D rb = target.GetComponent<Rigidbody2D>();

    	    if(rb) {
		// Debug.DrawRay(target.transform.position, direction * force, Color.yellow, 5);
		// Debug.Log("Pushing " + target.name);
    		rb.AddForce(direction * force, ForceMode2D.Impulse);
    	    }
    	}
    }
}
