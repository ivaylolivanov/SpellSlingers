using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Weapons/SlashWeapon")]
public class SlashingWeapon : Weapon {

    public override void Hit(Transform attackPoint, int ignoreMask) {
        Collider2D[] targets = Physics2D.OverlapCircleAll(
            attackPoint.position,
            range,
            ignoreMask
        );
        foreach(Collider2D target in targets) {
            Damage(target);
        }
    }

    private void Damage(Collider2D target) {
        ObjectStats targetStats = target.GetComponent<ObjectStats>();
        if(targetStats) {
            targetStats.TakeDamage(damage);
        }
    }
}
