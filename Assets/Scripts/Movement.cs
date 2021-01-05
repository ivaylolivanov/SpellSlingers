using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Rigidbody2D rb;
    private ObjectStats stats;
    private float speed;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<ObjectStats>();
        speed = stats.movementSpeed.GetValue();
    }

    public void Move(Vector2 movementInput) {
        Vector2 step = movementInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + step);
    }

    public void AutoMove(Transform target) {
        Vector2 lookDirection = (Vector2)target.position - rb.position;
        float lookAngleRads = Mathf.Atan2(lookDirection.y, lookDirection.x);
        float lookAngleDeg = Mathf.Rad2Deg * lookAngleRads;
        float lookDirDegPov = lookAngleDeg - 90f;

        rb.rotation = lookDirDegPov;
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );
    }
}
