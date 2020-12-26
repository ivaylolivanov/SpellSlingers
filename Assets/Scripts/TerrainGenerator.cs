using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    [SerializeField] private GameObject lavaTile;
    [SerializeField] private GameObject groundTile;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float randomFactor;
    [SerializeField] private float shrinkTime = 60f;

    private bool waiting = false;
    private int currentWidth;
    private int currentHeight;
    private Queue<GameObject> toDelete;

    void Start() {
        currentWidth = width;
        currentHeight = height;
        toDelete = new Queue<GameObject>();
    }

    void Update() {
        if (currentWidth > 0 && currentHeight > 0 && ! waiting) {
            waiting = true;
            while(toDelete.Count > 0) {
                Destroy(toDelete.Dequeue());
            }
            StartCoroutine(GenerateTerrain(currentWidth, currentHeight));
            --currentWidth;
            --currentHeight;
        }

    }

    private IEnumerator GenerateTerrain(int w, int h) {
        GameObject tileToInstantiate = groundTile;
        for (int x = -w / 2; x < w / 2; ++x) {
            for (int y = -h / 2; y < h / 2; ++y) {
                if (
                    y <= Random.Range((-h / 2) * randomFactor, (-h / 2 + 5) * randomFactor)
                    || y >= Random.Range((h / 2) * randomFactor, (h / 2 + 5) * randomFactor)
                ) {
                    tileToInstantiate = lavaTile;
                }
                else if (
                    x <= Random.Range((-w / 2) * randomFactor, (-w / 2 + 5) * randomFactor)
                    || x >= Random.Range((w / 2) * randomFactor, (w / 2 + 5) * randomFactor)
                ) {
                    tileToInstantiate = lavaTile;
                }
                else {
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

        waiting = false;
    }
}
