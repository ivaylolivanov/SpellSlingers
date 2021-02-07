using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private List<KeyCode> abilityHotkeys;

    private Rigidbody2D rb;

    private Combat combat;
    private Movement movement;

    private Vector2 mouseWorldPosition;
    private Vector2 movementInput;

    private Camera mainCamera;
    private Animator animator;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        combat = GetComponent<Combat>();
        movement = GetComponent<Movement>();
        mainCamera = Camera.main;
	animator = GetComponent<Animator>();
    }

    void Update() {
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(
            Input.mousePosition
        );

        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");
	if(movementInput != Vector2.zero) {
	    animator.SetFloat("horizontal", movementInput.x);
	    animator.SetFloat("vertical", movementInput.y);
	}
	animator.SetFloat("speed", movementInput.sqrMagnitude);

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
        float lookDirDegPOV = lookDirDeg - 90f;
        combat.RotateAttackPoint(lookDirDegPOV);
    }
}
