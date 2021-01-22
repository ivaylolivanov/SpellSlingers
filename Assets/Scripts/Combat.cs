﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Combat : MonoBehaviour {
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask[] hitIgnoreLayers;

    private CharacterStats stats;
    private List<string> abilitiesOnCooldown;

    private float nextWeaponAttack = 0;
    private Ability selectedAbility = null;

    private int hitIgnoreLayerMask = 0;

    public void Start() {
        stats = GetComponent<CharacterStats>();
        abilitiesOnCooldown = new List<string>();
        for (int i = 0; i < hitIgnoreLayers.Length; ++i) {
            hitIgnoreLayerMask |= hitIgnoreLayers[i];
        }
        hitIgnoreLayerMask = ~hitIgnoreLayerMask;
    }

    public void WeaponAttack() {
        if(Time.time >= nextWeaponAttack) {
            stats.GetEquippedWeapon().Hit(attackPoint, hitIgnoreLayerMask);
            nextWeaponAttack = Time.time
                + stats.GetEquippedWeapon().attackSpeed;
        }
    }

    public void SelectAbility(int abilityIndex) {
        selectedAbility = stats.GetAbility(abilityIndex);
    }

    public IEnumerator CastAbility() {
        if ((selectedAbility != null)
            && !abilitiesOnCooldown.Contains(selectedAbility.abilityName)
        ) {
            abilitiesOnCooldown.Add(selectedAbility.abilityName);
            selectedAbility.Execute(attackPoint);
            yield return new WaitForSeconds(selectedAbility.cooldown);
            abilitiesOnCooldown.Remove(selectedAbility.abilityName);
            selectedAbility = null;
        }
    }

    public void SetHitIgnoreLayerMask(int mask) {
        hitIgnoreLayerMask = mask;
    }
}