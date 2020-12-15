using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject {
    public string aName = "New Ability";
    public float aRange;
    public float aForce;
    public float aCooldown;

    public abstract void Initialize();
    public abstract void Execute(Transform location);

    protected bool aInCooldown;

    public IEnumerator Cooldown(MonoBehaviour runner) {
        yield return new WaitForSeconds(aCooldown);
        aInCooldown = false;
    }

    public bool IsInCooldown() {
        return this.aInCooldown;
    }
}
