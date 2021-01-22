using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    [SerializeField] private GameObject lavaTile;
    [SerializeField] private GameObject groundTile;
    [SerializeField] private GameObject[] obstacles;

    [SerializeField] private int width = 50;
    [SerializeField] private int height = 50;
    [SerializeField] private int groundWidth = 25;
    [SerializeField] private int groundHeight = 25;
    [SerializeField] private float randomFactor = 5f;
    [SerializeField] private float shrinkTime = 60f;
    [SerializeField] private float obstacleSpawnChance = 0.7f;

    private bool waiting = false;
    private Vector2 terrainCenter;
    private float shrinkRadius;
    private List<GameObject> groundTiles;

    void Start() {
        terrainCenter = new Vector2(0, 0);
        shrinkRadius = Mathf.Min(groundWidth, groundHeight) / 2;
        groundTiles = new List<GameObject>();
        GenerateTerrain();
    }

    void Update() {
        if (! waiting && shrinkRadius > 0) {
            waiting = true;
            StartCoroutine(ShrinkGround());
        }
    }

    private IEnumerator ShrinkGround() {
        List<GameObject> tilesToDelete = new List<GameObject>();
        foreach(GameObject tile in groundTiles) {
            float distance = (
                terrainCenter - (Vector2)tile.transform.position
            ).sqrMagnitude;
            if(distance > shrinkRadius * shrinkRadius) {
                Vector2 tilePosition = tile.transform.position;
                tilesToDelete.Add(tile);
                Destroy(tile);
                GameObject newTile = Instantiate(
                    lavaTile,
                    tilePosition,
                    Quaternion.identity
                );
                newTile.transform.parent = this.transform;
            }
        }

        foreach(GameObject tile in tilesToDelete) {
            groundTiles.Remove(tile);
        }

        yield return new WaitForSeconds(shrinkTime);

        --shrinkRadius;
        waiting = false;
    }

    private void GenerateTerrain() {
        GameObject tileToInstantiate = lavaTile;
        for (int x = (-width / 2); x < (width / 2); ++x) {
            for (int y = (-height / 2); y < (height / 2); ++y) {
                float groundHeightUpperLimit = Random.Range(
                    (groundHeight / 2),
                    (groundHeight / 2 - randomFactor)
                );
                float groundHeightLowerLimit = Random.Range(
                    (-groundHeight / 2),
                    (-groundHeight / 2 + randomFactor)
                );
                float groundWidthLeftLimit = Random.Range(
                    (-groundWidth / 2),
                    (-groundWidth / 2 + randomFactor)
                );
                float groundWidthRightLimit = Random.Range(
                    (groundWidth / 2 - randomFactor),
                    (groundWidth / 2)
                );
                bool isTileGround = x >= groundWidthLeftLimit
                    && x <= groundWidthRightLimit
                    && y >= groundHeightLowerLimit
                    && y <= groundHeightUpperLimit;

                tileToInstantiate = lavaTile;
                if(isTileGround) {
                    tileToInstantiate = groundTile;
                }

                Vector2 newTilePosition = new Vector2(x, y);
                GameObject tile = Instantiate(
                    tileToInstantiate,
                    newTilePosition,
                    Quaternion.identity
                );

                if(isTileGround) {
                    if(Random.value >= obstacleSpawnChance) {
                        int obstacleIndex = (int)Random.Range(0, obstacles.Length);
                        GameObject obstacle = Instantiate(
                            obstacles[obstacleIndex],
                            newTilePosition,
                            Quaternion.identity
                        );
                        obstacle.transform.parent = this.transform;
                    }
                    groundTiles.Add(tile);
                }
                tile.transform.parent = this.transform;
            }
        }
    }
}
