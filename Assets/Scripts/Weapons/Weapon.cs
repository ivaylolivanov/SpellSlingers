using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject {
    public string weaponName = "New Weapon";
    public float range;
    public float force;
    public float attackSpeed;
    public int damage;

    public abstract void Hit(Transform attackPoint, int ignoreMask);
}
