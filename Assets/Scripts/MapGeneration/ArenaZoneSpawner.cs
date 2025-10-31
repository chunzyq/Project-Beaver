using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ArenaZoneSpawner : MonoBehaviour
{
    public List<GameObject> zonePrefabs;
    public int numberOfZones = 5;
    public Tilemap groundTilemap;

    public void SpawnZones(TileBase[,] mapData, TileBase grassTile, int width, int height)
    {
        for (int i = 0; i < numberOfZones; i++)
        {
            int x = Random.Range(5, width - 5);
            int y = Random.Range(5, height - 5);

            if (mapData[x, y] != grassTile)
            {
                continue;
            }

            Vector3 worldPos = groundTilemap.CellToWorld(new Vector3Int(x, y, 0)) + new Vector3(0.5f, 0.5f, 0);
            Instantiate(zonePrefabs[Random.Range(0, zonePrefabs.Count)], worldPos, Quaternion.identity);
        }
    }
}
