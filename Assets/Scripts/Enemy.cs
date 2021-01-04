using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Weapon weapon;

    [Header("Targeting configuration variables")]
    [SerializeField] private float aggroRange = 5f;
    [SerializeField] private LayerMask targetLayer;

    private Movement movement;
    void Start() {
        if (targetLayer <= 0) {
            targetLayer = LayerMask.GetMask("Player");
        }
        movement = GetComponent<Movement>();
        weapon.Initialize(transform);
    }

    void Update() {
        Collider2D[] targets = Physics2D.OverlapCircleAll(
            transform.position,
            aggroRange,
            targetLayer
        );

        if (targets.Length > 0)
        {
            Transform target = FindClosestTarget(targets);
            float distance = GetDistanceToTarget(target);
            if(distance > weapon.aRange) {
                movement.AutoMove(target);
            }
            if (!weapon.IsInCooldown())
            {
                weapon.Hit(attackPoint);
                StartCoroutine(weapon.Cooldown(this));
            }
        }
    }

    private Transform FindClosestTarget(Collider2D[] targets) {
        Transform result = null;
        float minDistance = float.MaxValue;

        foreach(Collider2D target in targets) {
            float distance = GetDistanceToTarget(target.transform);
            if(minDistance > distance) {
                minDistance = distance;
                result = target.transform;
            }
        }

        return result;
    }

    private float GetDistanceToTarget(Transform target) {
        float result = Vector2.Distance(target.position, this.transform.position);

        Collider2D collider = target.GetComponent<Collider2D>();
        if(collider) {
            Vector2 closestPoint = collider.ClosestPoint(this.transform.position);
            result = Vector2.Distance(closestPoint, this.transform.position);
        }

        return result;
    }
}
