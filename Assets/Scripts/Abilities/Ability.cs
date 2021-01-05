using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject {
    public string name = "New Ability";
    public float range;
    public float force;
    public float cooldown;
    public AbilityEffect effect;

    public abstract void Initialize();
    public abstract void Execute(Transform location);

    protected bool inCooldown;

    public IEnumerator Cooldown(MonoBehaviour runner) {
        yield return new WaitForSeconds(cooldown);
        inCooldown = false;
    }

    public bool IsInCooldown() {
        return this.inCooldown;
    }
}
