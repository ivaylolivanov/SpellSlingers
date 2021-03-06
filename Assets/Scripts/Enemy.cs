﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Weapon weapon;

    [Header("Targeting configuration variables")]
    [SerializeField] private float aggroRange = 5f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private LayerMask raycastIgnoreLayers;

    private Movement movement;
    private int raycastIgnoreLayerMask = 0;
    private Combat combat;

    void Start() {
        combat = GetComponent<Combat>();

        if (targetLayer <= 0) {
            targetLayer = LayerMask.GetMask("Player");
        }

        raycastIgnoreLayerMask = ~raycastIgnoreLayers.value;
        combat.SetHitIgnoreLayerMask(raycastIgnoreLayerMask);

        movement = GetComponent<Movement>();
    }

    void Update() {
        Collider2D[] targets = Physics2D.OverlapCircleAll(
            transform.position,
            aggroRange,
            targetLayer
        );

        if (targets.Length > 0) {
            Transform target = FindClosestTarget(targets);
            if (target) {
                float distance = GetDistanceToTarget(target);
                if (distance > weapon.range * weapon.range) {
                    movement.AutoMove(target);
                }
                else {
                    combat.WeaponAttack();
                }
            }
        }
    }

    private Transform FindClosestTarget(Collider2D[] targets) {
        Transform result = null;
        float minDistance = float.MaxValue;

        foreach(Collider2D target in targets) {
            if (! IsTargetOnLineOfSight(target)) { continue; }

            float distance = GetDistanceToTarget(target.transform);
            if(minDistance > distance) {
                minDistance = distance;
                result = target.transform;
            }
        }

        return result;
    }

    private bool IsTargetOnLineOfSight(Collider2D target) {
        bool result = false;
        Vector2 closestPoint = target.ClosestPoint(this.transform.position);
        Vector2 targetDirection = GetTargetDirection(closestPoint);

        RaycastHit2D lineOfSight = Physics2D.Raycast(
            attackPoint.position,
            targetDirection,
            aggroRange,
            raycastIgnoreLayerMask
        );

        if(lineOfSight) {
            result = GameObject.ReferenceEquals(
                lineOfSight.transform.gameObject,
                target.transform.gameObject
            );
        }

        return result;
    }

    private Vector2 GetTargetDirection(Vector2 target) {
        Vector2 direction = (target - (Vector2)attackPoint.position).normalized;
        float angleRads = Mathf.Atan2(direction.y, direction.x);

        direction = Quaternion.Euler(0, angleRads, 0) * direction;

        return direction;
    }

    private float GetDistanceToTarget(Transform target) {
        float result = (target.position - this.transform.position).sqrMagnitude;

        Collider2D collider = target.GetComponent<Collider2D>();
        if(collider) {
            Vector2 closestPoint = collider.ClosestPoint(this.transform.position);
            result = (closestPoint - (Vector2)this.transform.position).sqrMagnitude;
        }

        return result;
    }
}
