using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float aggroRange = 5f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Weapon weapon;

    private Rigidbody2D rb;

    void Start() {
        if (targetLayer <= 0) {
            targetLayer = LayerMask.GetMask("Player");
        }
        weapon.Initialize(transform);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Collider2D[] targets = Physics2D.OverlapCircleAll(
            transform.position,
            aggroRange,
            targetLayer
        );
        if (targets.Length > 0) {
            Transform target = FindClosestTarget(targets);
            ChaseTarget(target);
            if(! weapon.IsInCooldown()) {
                weapon.Hit(attackPoint);
                StartCoroutine(weapon.Cooldown(this));
            }
        }
    }

    Transform FindClosestTarget(Collider2D[] targets) {
        Transform result = null;
        float minDistance = float.MaxValue;

        foreach(Collider2D target in targets) {
            float distance = Vector2.Distance(
                transform.position,
                target.transform.position
            );
            if(minDistance > distance) {
                minDistance = distance;
                result = target.transform;
            }
        }

        return result;
    }

    private void ChaseTarget(Transform target) {
        float distance = GetDistanceToTarget(target);
        if (distance > weapon.aRange)
        {
            Vector2 lookDirection = (Vector2)target.position - rb.position;
            float lookAngleRads = Mathf.Atan2(lookDirection.y, lookDirection.x);
            float lookAngleDeg = Mathf.Rad2Deg * lookAngleRads;
            float lookDirDegPov = lookAngleDeg - 90f;

            rb.rotation = lookDirDegPov;
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.position,
                moveSpeed * Time.deltaTime
            );
        }
    }

    private float GetDistanceToTarget(Transform target) {
        float result = Vector2.Distance(target.position, attackPoint.position);

        Collider2D collider = target.GetComponent<Collider2D>();
        if(collider) {
            Vector2 closestPoint = collider.ClosestPoint(attackPoint.position);
            result = Vector2.Distance(closestPoint, attackPoint.position);
        }

        return result;
    }
}
