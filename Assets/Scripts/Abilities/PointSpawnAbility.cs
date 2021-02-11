using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Abilities/PointSpawnAbility")]
public class PointSpawnAbility : Ability {
    public int duration;
    public GameObject spawnObject;

    public override void Initialize() {
        SpawnableObject obj = spawnObject.GetComponent<SpawnableObject>();
        obj.SetRange(range);
        obj.SetForce(force);
    }

    public override void Execute(Vector2 location) {
        GameObject spawnedObject = Instantiate(
            spawnObject,
            location,
	    Quaternion.identity
        );

	SpawnableObject obj = spawnedObject.GetComponent<SpawnableObject>();
	obj.Die(duration);
    }

    public override void Execute(Transform location) {}
}
