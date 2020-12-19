using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private float speed = 3f;

    private Rigidbody2D rb;
    private Vector2 destination;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerStay2D(Collider2D col) {
        if(col.tag == "Player") {
            Vector2 playerLastSeenPosition = col.gameObject.transform.position;
            Vector2 lookDir = playerLastSeenPosition - rb.position;
            float lookAngleRads = Mathf.Atan2(lookDir.y, lookDir.x);
            float lookAngleDirDeg = lookAngleRads * Mathf.Rad2Deg;
            float lookDirDegPOV = lookAngleDirDeg - 90f;

            rb.rotation = lookDirDegPOV;

            transform.position = Vector2.MoveTowards(
                transform.position,
                playerLastSeenPosition,
                speed * Time.deltaTime
            );
        }
    }
}
