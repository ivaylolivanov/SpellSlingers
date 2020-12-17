using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Player States")]
    [SerializeField] int hitPoints = 100;
    [SerializeField] float speed = 20f;

    Rigidbody2D rb;
    Vector2 movement;
    Vector2 mouseWorldPosition;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector2(horizontal, vertical);

        mouseWorldPosition = Camera.main.ScreenToWorldPoint(
            Input.mousePosition
        );
    }

    void FixedUpdate() {
        Vector2 step = movement * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + step);

        Look2Mouse();
    }

    private void Look2Mouse() {
        Vector2 lookDir = mouseWorldPosition - rb.position;
        float lookAngleRads = Mathf.Atan2(lookDir.y, lookDir.x);
        float lookDirDeg = lookAngleRads * Mathf.Rad2Deg;
        float lookDirDegPOV = lookDirDeg - 90f;
        rb.rotation = lookDirDegPOV;
    }
}
