using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEffect : ScriptableObject {
    public string name = "New Effect";
    public float duration;

    public abstract IEnumerator Apply(ObjectStats target);
}
