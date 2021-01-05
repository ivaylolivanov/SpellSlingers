using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Ability effects/Movement effect")]
public class MovementEffect : AbilityEffect {
    public int slowAmount;

    public override IEnumerator Apply(ObjectStats target) {
        target.movementSpeed.AddModifier(-slowAmount);
        yield return new WaitForSeconds(duration);
        target.movementSpeed.RemoveModifier(-slowAmount);
    }
}
