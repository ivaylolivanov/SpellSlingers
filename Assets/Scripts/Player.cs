using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D rb;
    private Movement movement;

    private Vector2 mouseWorldPosition;
    private Vector2 movementInput;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
    }

    void Update() {
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(
            Input.mousePosition
        );

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementInput = new Vector2(horizontal, vertical);
    }

    void FixedUpdate() {
        Look2Mouse();
        movement.Move(movementInput);
    }

    private void Look2Mouse() {
        Vector2 lookDir = mouseWorldPosition - rb.position;
        float lookAngleRads = Mathf.Atan2(lookDir.y, lookDir.x);
        float lookDirDeg = lookAngleRads * Mathf.Rad2Deg;
        float lookDirDegPOV = lookDirDeg - 90f;
        rb.rotation = lookDirDegPOV;
    }
}
