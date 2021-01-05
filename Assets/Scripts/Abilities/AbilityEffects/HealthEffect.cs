using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Ability effects/Health effect")]
public class HealthEffect : AbilityEffect {
    public int amount;
    public int ticks;

    public override IEnumerator Apply(ObjectStats target) {
        float tickDuration = duration / ticks;
        int amountPerTick = amount / ticks;

        for (int i = 0; i < ticks; ++i) {
            yield return new WaitForSeconds(tickDuration);
            target.TakeDamage(amountPerTick);
        }
    }
}
