using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : ObjectStats {
    [SerializeField] private Weapon equippedWeapon;
    [SerializeField] private List<Ability> abilities;

    void Start() {
	foreach(Ability ability in abilities) {
	    ability.Initialize();
	}
    }

    public Weapon EquipWeapon(Weapon newWeapon) {
        Weapon oldWeapon = equippedWeapon;
        equippedWeapon = newWeapon;

        return oldWeapon;
    }

    public Weapon GetEquippedWeapon() {
        return equippedWeapon;
    }

    public void AddAbility(Ability newAbility) {
        abilities.Add(newAbility);
    }

    public void RemoveAbility(Ability ability) {
        abilities.Remove(ability);
    }

    public Ability GetAbility(int abilityIndex) {
        if(abilityIndex >= 0 && abilityIndex < abilities.Count) {
             return abilities[abilityIndex];
        }
        return null;
    }
}
