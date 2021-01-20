using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapTerrainGenerator : MonoBehaviour {
    [SerializeField] private int width          = 50;
    [SerializeField] private int height         = 50;
    [SerializeField] private int groundWidth    = 25;
    [SerializeField] private int groundHeight   = 25;
    [SerializeField] private float shrinkTime   = 60f;
    [SerializeField] private float randomFactor = 5f;
    [Range(0, 1)] [SerializeField] private float obstacleSpawnChance = 0.95f;
    [SerializeField] private List<GameObject> obstacles;
    [SerializeField] private int lavaDamage = 5;
    [SerializeField] private float lavaDamagePeriod = 3f;
    [SerializeField] private Tile lavaTile;
    [SerializeField] private Tile groundTile;

    private Tilemap tilemap;
    private List<Vector3Int> groundTiles;
    private Vector3Int terrainCenter;

    private bool shouldShrink;
    private float shrinkRadius;
    private List<GameObject> objectsOnTheMap;
    private List<ObjectStats> lavaTargets;

    void Start() {
        terrainCenter = new Vector3Int(0, 0, 0);

        tilemap = GetComponent<Tilemap>();
        tilemap.ClearAllTiles();
        groundTiles = new List<Vector3Int>();
        GenerateTilemap();

        shouldShrink = true;
        shrinkRadius = Mathf.Max(groundWidth, groundHeight) / 2;

        objectsOnTheMap = new List<GameObject>();

        lavaTargets = new List<ObjectStats>();
    }

    void Update() {
        if (shouldShrink && shrinkRadius >= 0) {
            shouldShrink = false;
            StartCoroutine(ShrinkGround());
            --shrinkRadius;
        }
    }

    void FixedUpdate() {
        foreach(GameObject objectOnTheMap in objectsOnTheMap) {
            Vector3Int position = Vector3Int.FloorToInt(objectOnTheMap.transform.position);
            ObjectStats stats = objectOnTheMap.GetComponent<ObjectStats>();
            if(stats && (! groundTiles.Contains(position))) {
                StartCoroutine(DoLavaDamage(stats));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        objectsOnTheMap.Add(collider.gameObject);
    }

    void OnTriggerExit2D(Collider2D collider) {
        objectsOnTheMap.Remove(collider.gameObject);
    }

    private IEnumerator ShrinkGround() {
        List<Vector3Int> swappedTilePositions = new List<Vector3Int>();
        foreach(Vector3Int tilePosition in groundTiles) {
            float distance = Vector3Int.Distance(terrainCenter, tilePosition);
            if(distance > shrinkRadius) {
                swappedTilePositions.Add(tilePosition);
                tilemap.SetTile(tilePosition, lavaTile);
            }
        }

        foreach(Vector3Int swappedTile in swappedTilePositions) {
            groundTiles.Remove(swappedTile);
        }

        yield return new WaitForSeconds(shrinkTime);
        shouldShrink = true;
    }

    private void GenerateTilemap() {
        Tile tileToInstantiate = lavaTile;
        for (int x = (- width / 2); x < (width / 2); ++x) {
            for (int y = (- height / 2); y < height / 2; ++y) {
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

                Vector3Int newTilePosition = new Vector3Int(x, y, 0);

                tileToInstantiate = lavaTile;
                if(isTileGround) {
                    tileToInstantiate = groundTile;
                    groundTiles.Add(newTilePosition);
                    if(Random.value >= obstacleSpawnChance) {
                        int obstacleIndex = (int)Random.Range(0, obstacles.Count);
                        GameObject obstacle = Instantiate(
                            obstacles[obstacleIndex],
                            newTilePosition,
                            Quaternion.identity
                        );
                        obstacle.transform.parent = this.transform;
                    }
                }

                tilemap.SetTile(newTilePosition, tileToInstantiate);
            }
        }

        tilemap.RefreshAllTiles();
    }

    private IEnumerator DoLavaDamage(ObjectStats target) {
        if(! lavaTargets.Contains(target)) {
            lavaTargets.Add(target);
            target.TakeDamage(lavaDamage);
            yield return new WaitForSeconds(lavaDamagePeriod);
            lavaTargets.Remove(target);
        }
    }
}
