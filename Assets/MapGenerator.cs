using System.Collections;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Settings")]
    [SerializeField] private int width = 100;
    [SerializeField] private int height = 100;

    [Header("Noise Settings")]
    [SerializeField] private float scale = 10f;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;

    [Header("Tilemaps")]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap waterTilemap;

    [Header("Tiles")]
    [SerializeField] private TileBase grassTile;
    [SerializeField] private TileBase waterTile;

    [Header("Player")]
    [SerializeField] private GameObject player;

    [Header("NavMesh")]
    [SerializeField] private NavMeshSurface navMeshSurface;

    private TileBase[,] mapData;

    void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        offsetX = UnityEngine.Random.Range(0f, 9999f);
        offsetY = UnityEngine.Random.Range(0f, 9999f);

        mapData = new TileBase[width, height];

        groundTilemap.ClearAllTiles();
        waterTilemap.ClearAllTiles();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * scale + offsetX;
                float yCoord = (float)y / height * scale + offsetY;

                float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

                if (noiseValue < 0.4f)
                {
                    waterTilemap.SetTile(new Vector3Int(x, y, 0), waterTile);
                    mapData[x, y] = waterTile;
                }
                else
                {
                    groundTilemap.SetTile(new Vector3Int(x, y, 0), grassTile);
                    mapData[x, y] = grassTile;
                }
            }
        }

        groundTilemap.RefreshAllTiles();
        waterTilemap.RefreshAllTiles();

        StartCoroutine(RebuildNavMeshLater());

        StartCoroutine(SpawnPlayerAfterNavMesh());
    }

        private IEnumerator RebuildNavMeshLater()
        {
            yield return new WaitForEndOfFrame();

            if (navMeshSurface != null)
            {
                navMeshSurface.BuildNavMesh();
            }
        }

        private IEnumerator SpawnPlayerAfterNavMesh()
        {
            yield return new WaitForSeconds(0.2f);

            Vector3Int spawnCell = FindSafeSpawnPoint();
            Vector3 spawnPos = groundTilemap.CellToWorld(spawnCell) + new Vector3(0.5f, 0.5f, 0);

            if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            {
                player.transform.position = hit.position;
            }
            else
            {
                player.transform.position = spawnPos;
            }
        }

        private Vector3Int FindSafeSpawnPoint()
        {
            for (int attempts = 0; attempts < 1000; attempts++)
            {
                int x = UnityEngine.Random.Range(1, width - 1);
                int y = UnityEngine.Random.Range(1, height - 1);

                if (!IsWalkable(x, y))
                    continue;

                bool safe = true;
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (!IsWalkable(x + dx, y + dy))
                            safe = false;
                    }
                }

                if (safe)
                    return new Vector3Int(x, y, 0);
            }
            return new Vector3Int(width / 2, height / 2, 0);
        }
        
        private bool IsWalkable(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
            return false;

            return mapData[x, y] == grassTile;
        }
}
