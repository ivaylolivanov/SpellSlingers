using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private List<KeyCode> abilityHotkeys;
    [SerializeField] private List<Sprite> idleSprites;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Combat combat;
    private Movement movement;

    private Vector2 mouseWorldPosition;
    private Vector2 movementInput;

    private Camera mainCamera;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        combat = GetComponent<Combat>();
        movement = GetComponent<Movement>();
        mainCamera = Camera.main;
    }

    void Update() {
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(
            Input.mousePosition
        );

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementInput = new Vector2(horizontal, vertical);

        for (int i = 0; i < abilityHotkeys.Count; ++i) {
            if (Input.GetKeyDown(abilityHotkeys[i])) {
                combat.SelectAbility(i);
            }
        }

        if(Input.GetMouseButtonDown(0)) {
            StartCoroutine(combat.CastAbility());
        }

        if(Input.GetMouseButtonDown(1)) {
            combat.WeaponAttack();
        }
    }

    void FixedUpdate() {
        Look2Mouse();
        movement.Move(movementInput);
    }

    private void Look2Mouse() {
        Vector2 lookDir = mouseWorldPosition - rb.position;
        float lookAngleRads = Mathf.Atan2(lookDir.y, lookDir.x);
        float lookDirDeg = lookAngleRads * Mathf.Rad2Deg;

        if(lookDirDeg > -45 && lookDirDeg <= 45) {
            spriteRenderer.sprite = idleSprites[1];
            spriteRenderer.flipX = false;
        }
        else if(lookDirDeg > 45 && lookDirDeg <= 135) {
            spriteRenderer.sprite = idleSprites[2];
            spriteRenderer.flipX = false;
        }
        else if(lookDirDeg > 135 || lookDirDeg <= -135) {
            spriteRenderer.sprite = idleSprites[1];
            spriteRenderer.flipX = true;
        }
        else {
            spriteRenderer.sprite = idleSprites[0];
            spriteRenderer.flipX = false;
        }

        float lookDirDegPOV = lookDirDeg - 90f;
        combat.RotateAttackPoint(lookDirDegPOV);
    }
}
