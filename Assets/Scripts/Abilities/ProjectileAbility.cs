using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability {
    public int damage;
    public GameObject projectile;

    public override void Initialize() {
        projectile.GetComponent<Projectile>().SetDamage(damage);
        projectile.GetComponent<Projectile>().SetRange(range);
        projectile.GetComponent<Projectile>().SetEffect(effect);
    }

    public override void Execute(Transform location) {
        GameObject abilityProjectile = Instantiate(
            projectile,
            location.position,
            Quaternion.identity
        );
        Rigidbody2D projectileRb
            = abilityProjectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(
            location.up * force,
            ForceMode2D.Impulse
        );
    }
}
