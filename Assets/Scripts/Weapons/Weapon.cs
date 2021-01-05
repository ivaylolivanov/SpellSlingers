using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject {
    public string aName = "New Weapon";
    public float aRange;
    public float aForce;
    public float aCooldown;

    public abstract void Initialize(Transform owner);
    public abstract void Hit(Transform attackPoint);

    protected bool aInCooldown;
    protected Transform aOwner;

    public IEnumerator Cooldown(MonoBehaviour runner) {
        yield return new WaitForSeconds(aCooldown);
        aInCooldown = false;
    }

    public bool IsInCooldown() {
        return this.aInCooldown;
    }
}
