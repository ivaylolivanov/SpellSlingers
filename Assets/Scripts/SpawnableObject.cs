using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour {
    public string objectName = "New Object";

    public abstract void SetForce(float force);
    public abstract void SetRange(float range);
    public abstract void Die(float delay = 0);
}
