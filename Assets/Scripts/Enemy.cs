using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Main Stats")]
    [SerializeField] private float speed = 3f;

    [Header("Ranges")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float aggroRange = 5f;

    [Header("Attack stats")]
    [SerializeField] private int damage = 10;
    [SerializeField] private float timeBetweenAttacks = .5f;

    [Header("Attack helper objects")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask layerToAttack;

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private int presentTargets = 0;
    private Collider2D[] targets2Attack = null;
    private bool hasAttacked = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        DetectTargets();
        if(this.presentTargets > 0) {
            ChaseTarget();
        }
        if(this.targets2Attack != null && this.targets2Attack.Length > 0 && ! this.hasAttacked) {
            this.hasAttacked = true;
            StartCoroutine(Attack());
        }
    }

    void DetectTargets() {
        Collider2D[] detectedTargets = Physics2D.OverlapCircleAll(
            transform.position,
            aggroRange,
            layerToAttack
        );
        this.presentTargets = detectedTargets.Length;

        if(this.presentTargets > 0) {
            this.targets2Attack = Physics2D.OverlapCircleAll(
                attackPoint.position,
                attackRange,
                layerToAttack
            );
        }
        this.targetPosition = FindClosestTarget(detectedTargets);
    }

    Vector2 FindClosestTarget(Collider2D[] detectedTargets) {
        float minDistance = float.MaxValue;
        Vector2 result = Vector2.zero;

        foreach(Collider2D target in detectedTargets) {
            float distance = Vector2.Distance(
                transform.position,
                target.transform.position
            );
            if(minDistance > distance) {
                minDistance = distance;
                result = target.transform.position;
            }
        }

        return result;
    }

    void ChaseTarget() {
        Vector2 lookDirection = this.targetPosition - rb.position;
        float lookAngleRads = Mathf.Atan2(lookDirection.y, lookDirection.x);
        float lookAngleDeg = Mathf.Rad2Deg * lookAngleRads;
        float lookDirDegPov = lookAngleDeg - 90f;

        rb.rotation = lookDirDegPov;
        transform.position = Vector2.MoveTowards(
            transform.position,
            this.targetPosition,
            speed * Time.deltaTime
        );
    }

    IEnumerator Attack() {
        yield return new WaitForSeconds(timeBetweenAttacks);
        foreach(Collider2D target in this.targets2Attack) {
            Health healthComponent = target.gameObject.GetComponent<Health>();
            if(healthComponent) {
                healthComponent.DoDamage(this.damage);
            }
        }
        this.hasAttacked = false;
    }
}
