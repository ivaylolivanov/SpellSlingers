using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour {
    [SerializeField] Ability ability;
    [SerializeField] Ability ability2;
    [SerializeField] Transform firePoint;

    bool onGlobalCooldown = false;

    void Start() {
        ability.Initialize();
        ability2.Initialize();
    }

    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            CastSpell(ability);
        }
        if(Input.GetMouseButtonDown(1)) {
            CastSpell(ability2);
        }
    }

    void CastSpell(Ability castingAbility) {
        if(! castingAbility.IsInCooldown()) {
            castingAbility.Execute(firePoint);
            StartCoroutine(castingAbility.Cooldown(this));
        }
    }
}
