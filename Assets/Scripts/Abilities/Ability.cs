using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject {
    public string abilityName = "New Ability";
    public float range;
    public float force;
    public float cooldown;
    public AbilityEffect effect;

    public abstract void Initialize();
    public abstract void Execute(Transform location);
    public abstract void Execute(Vector2 location);
}
