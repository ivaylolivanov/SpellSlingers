using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Weapons/SlashWeapon")]
public class SlashingWeapon : Weapon {
    public int damage = 10;

    public override void Initialize(Transform owner) {
        aOwner = owner;
        aInCooldown = false;
    }

    public override void Hit(Transform attackPoint) {
        Collider2D[] targets = Physics2D.OverlapCircleAll(
            attackPoint.position,
            aRange
        );
        foreach(Collider2D target in targets) {
            if(target.transform == aOwner) {
                continue;
            }
            Damage(target);
        }

        aInCooldown = true;
    }

    private void Damage(Collider2D target) {
        Health health = target.GetComponent<Health>();
        if(health) {
            health.DoDamage(damage);
        }
    }
}
