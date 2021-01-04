using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour {
    [SerializeField] private List<KeyCode> abilityKeyCodes;
    [SerializeField] private List<Ability> abilities;
    [SerializeField] private Transform firePoint;

    private Ability selectedAbility;
    void Start() {
        selectedAbility = abilities[0];
        foreach(Ability ability in abilities) {
            ability.Initialize();
        }
    }

    void Update() {
        for (int i = 0; i < abilityKeyCodes.Count; ++i) {
            if (Input.GetKeyDown(abilityKeyCodes[i])) {
                selectedAbility = abilities[i];
            }
        }

        if (selectedAbility && Input.GetMouseButtonDown(0)) {
            CastSpell();
        }
    }

    void CastSpell() {
        if(! selectedAbility.IsInCooldown()) {
            selectedAbility.Execute(firePoint);
            StartCoroutine(selectedAbility.Cooldown(this));
        }
    }
}
