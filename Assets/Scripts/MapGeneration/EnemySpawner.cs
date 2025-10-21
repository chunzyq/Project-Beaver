using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public List<GameObject> enemyPrefabs;

    [Header("Spawn Settings")]
    public int totalEnemies = 10;
    public float minDistanceFromPlayer = 8f;
    public float minDistanceBetweenEnemies = 3f;

    [Header("References")]
    public Tilemap groundTilemap;
    public GameObject player;

    private List<Vector3> usedPositions = new List<Vector3>();

    // ------------------------------------------------------------
    public void SpawnEnemies(TileBase[,] mapData, TileBase grassTile, int width, int height)
    {
        int spawned = 0;
        int safety = 0;

        while (spawned < totalEnemies && safety < 2000)
        {
            safety++;

            int x = Random.Range(1, width - 1);
            int y = Random.Range(1, height - 1);

            // Проверка тайла
            if (mapData[x, y] != grassTile)
                continue;

            Vector3 worldPos = groundTilemap.CellToWorld(new Vector3Int(x, y, 0)) + new Vector3(0.5f, 0.5f, 0);

            // Проверка расстояния до игрока
            if (Vector3.Distance(worldPos, player.transform.position) < minDistanceFromPlayer)
                continue;

            // Проверка расстояния до других врагов
            bool tooClose = false;
            foreach (var pos in usedPositions)
            {
                if (Vector3.Distance(worldPos, pos) < minDistanceBetweenEnemies)
                {
                    tooClose = true;
                    break;
                }
            }
            if (tooClose) continue;

            // Проверка, что позиция на NavMesh
            if (!NavMesh.SamplePosition(worldPos, out NavMeshHit hit, 1f, NavMesh.AllAreas))
                continue;

            // Выбираем случайный тип врага
            GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            Instantiate(prefab, hit.position, Quaternion.identity);

            usedPositions.Add(worldPos);
            spawned++;
        }

        Debug.Log($"Spawned {spawned}/{totalEnemies} enemies.");
    }
}
