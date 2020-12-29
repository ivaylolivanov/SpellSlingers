using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    [SerializeField] private GameObject lavaTile;
    [SerializeField] private GameObject groundTile;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int groundWidth;
    [SerializeField] private int groundHeight;
    [SerializeField] private float randomFactor;
    [SerializeField] private float shrinkTime = 60f;

    private bool waiting = false;
    private int currentGroundWidth;
    private int currentGroundHeight;
    private Queue<GameObject> toDelete;

    void Start() {
        currentGroundWidth = groundWidth;
        currentGroundHeight = groundHeight;
        toDelete = new Queue<GameObject>();
    }

    void Update() {
        if (currentGroundWidth > 0 && currentGroundHeight > 0 && ! waiting) {
            waiting = true;
            while(toDelete.Count > 0) {
                Destroy(toDelete.Dequeue());
            }
            StartCoroutine(GenerateTerrain());
        }
    }

    private IEnumerator GenerateTerrain() {
        GameObject tileToInstantiate = lavaTile;
        for (int x = (-width / 2); x < (width / 2); ++x) {
            for (int y = (-height / 2); y < (height / 2); ++y) {
                float groundHeightUpperLimit = Random.Range(
                    (currentGroundHeight / 2),
                    (currentGroundHeight / 2 - randomFactor)
                );
                float groundHeightLowerLimit = Random.Range(
                    (-currentGroundHeight / 2),
                    (-currentGroundHeight / 2 + randomFactor)
                );
                float groundWidthLeftLimit = Random.Range(
                    (-currentGroundWidth / 2),
                    (-currentGroundWidth / 2 + randomFactor)
                );
                float groundWidthRightLimit = Random.Range(
                    (currentGroundWidth / 2 - randomFactor),
                    (currentGroundWidth / 2)
                );
                bool isTileGround = x >= groundWidthLeftLimit
                    && x <= groundWidthRightLimit
                    && y >= groundHeightLowerLimit
                    && y <= groundHeightUpperLimit;

                tileToInstantiate = lavaTile;
                if(isTileGround) {
                    tileToInstantiate = groundTile;
                }

                GameObject tile = Instantiate(
                    tileToInstantiate,
                    new Vector2(x, y),
                    Quaternion.identity
                );
                tile.transform.parent = this.transform;
                toDelete.Enqueue(tile);

            }
        }

        yield return new WaitForSeconds(shrinkTime);

        --currentGroundWidth;
        --currentGroundHeight;

        waiting = false;
    }
}
