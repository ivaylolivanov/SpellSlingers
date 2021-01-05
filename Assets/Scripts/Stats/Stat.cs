using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {
    public int baseValue;

    private List<int> modifiers = new List<int>();

    public int GetValue() {
        int result = baseValue;
        modifiers.ForEach(m => result += m);
        return result;
    }

    public void AddModifier(int modifier) {
        if(modifier != 0) {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(int modifier) {
        if(modifier != 0) {
            modifiers.Remove(modifier);
        }
    }
}
