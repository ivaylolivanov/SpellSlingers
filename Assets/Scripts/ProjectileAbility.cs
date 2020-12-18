using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/ProjectileAbility")]
public class ProjectileAbility : Ability {
    public int damage = 10;
    public GameObject projectile;

    public override void Initialize() {
        projectile.GetComponent<Projectile>().SetDamage(damage);
        projectile.GetComponent<Projectile>().SetRange(aRange);
        aInCooldown = false;
    }

    public override void Execute(Transform location) {
        if(!aInCooldown) {
            GameObject abilityProjectile = Instantiate(
                projectile,
                location.position,
                Quaternion.identity
            );
            Rigidbody2D projectileRb
                = abilityProjectile.GetComponent<Rigidbody2D>();
            projectileRb.AddForce(
                location.up * aForce,
                ForceMode2D.Impulse
            );

            aInCooldown = true;
        }
    }
}
