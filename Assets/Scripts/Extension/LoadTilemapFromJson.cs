using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public class LoadTilemapFromJson : MonoBehaviour
{
    private Tilemap tilemap;
    public Tile[] tilePrefabs;
    private int normalizeDistance = 0;
    void Awake()
    {
        createTilemap();
        LoadTilemapFromJsonFile(Application.persistentDataPath + "/" + tilemap.name.ToLower() + ".json");
        // LoadTilemapFromJsonFile(Application.persistentDataPath + "/" + "level2" + ".json");
    }
    public void createTilemap()
    {
        tilemap = new GameObject("Tilemap").AddComponent<Tilemap>();
        tilemap.name = this.gameObject.name; //SceneManager.GetActiveScene().name;
    }
    public void LoadTilemapFromJsonFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            Debug.Log(filePath.ToString());
            string json = File.ReadAllText(filePath);
            TilemapData tilemapData = JsonConvert.DeserializeObject<TilemapData>(json);

            foreach (var tileData in tilemapData.tiles)
            {
                Vector3Int tilePosition = new Vector3Int(tileData.x, tileData.y, 0);
                Tile tile = Array.Find(tilePrefabs, t => t.name == tileData.tileName);
                if (tile != null)
                {
                    tilemap.SetTile(tilePosition, tile);
                }
            }
            List<TileData> sortedTiles = tilemapData.tiles.OrderBy(tile => tile.x).ThenBy(tile => tile.y).ToList();
            sortedTiles = FlipHorizotalTilemap(sortedTiles);
            List<List<int>> map = TilemapToMap(sortedTiles);
            Debug.Log("map: " + JsonConvert.SerializeObject(map));
            int[] pos = null; //{ 2, 2 };
            findCurrentHallway(map, pos);           

            // Debug.Log("tilemapData: \n"+JsonConvert.SerializeObject(sortedTiles));
            // Debug.Log("tilemapData.tiles: \n"+JsonConvert.SerializeObject(tilemapData.tiles));
        }
        else
        {
            Debug.LogError("JSON file not found.");
        }
    }
    public List<TileData> FlipHorizotalTilemap(List<TileData> tilemap)
    {
        int minY = 1000000;
        for (int i = 0; i < tilemap.Count; i++)
        {
            TileData tileData = tilemap[i];
            if (tileData.y < minY) minY = tileData.y;
        }
        for (int i = 0; i < tilemap.Count; i++) {
            tilemap[i].y -= 2*Math.Abs(tilemap[i].y - minY);
        };
        return tilemap;
    }
    public List<List<int>> TilemapToMap(List<TileData> tilemap)
    {

        int minX = 1000000, minY = 1000000;
        int maxX = -1000000, maxY = -100000;
        for (int i = 0; i < tilemap.Count; i++)
        {
            TileData tileData = tilemap[i];
            if (tileData.x > maxX) maxX = tileData.x;
            if (tileData.x < minX) minX = tileData.x;
            if (tileData.y > maxY) maxY = tileData.y;
            if (tileData.y < minY) minY = tileData.y;
        }
        // normalize map
        normalizeDistance = Math.Min(minX, minY);
        if (normalizeDistance < 0)
        {
            for (int i = 0; i < tilemap.Count; i++) tilemap[i].x -= normalizeDistance;
            for (int i = 0; i < tilemap.Count; i++) tilemap[i].y -= normalizeDistance;
        }

        // map fixed size 10x10
        int n = 10;
        int m = 10;

        // generate map
        List<List<int>> map = new List<List<int>>();
        for (int i = 0; i <= m; i++)
        {
            List<int> row = new List<int>();
            for (int j = 0; j <= n; j++)
            {
                TileData result = tilemap.FirstOrDefault(tile => tile.x - minX == j && tile.y - minY == i);
                if (result != null)
                {
                    switch (result.tileName)
                    {
                        case "Start":
                            row.Add(1);
                            break;
                        case "EmptyStep":
                            row.Add(0);
                            break;
                        case "buff":
                            row.Add(2);
                            break;
                        case "obstacle":
                            row.Add(3);
                            break;
                        case "MysteryRoom":
                            row.Add(4);
                            break;
                        case "BossBear":
                            row.Add(5);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    row.Add(-1);
                }
            }
            map.Add(row);
        }
        return map;
    }
    public void findCurrentHallway(List<List<int>> map, int[]? startPosition)
    {
        int[] dirX = { 0, 0, -1, 1 }; // Down - Up - Left - Right
        int[] dirY = { -1, 1, 0, 0 }; // Down - Up - Left - Right
        int[] startPos = { 0, 0 };
        int m = map.Count;
        int n = map[0].Count;
        if (startPosition == null)
        {
            // find start position
            for (int row = 0; row < m; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    if (map[row][col] == 1)
                    {
                        startPos[0] = row;
                        startPos[1] = col;
                        break;
                    }
                }
            }
        }
        else
        {
            startPos[0] = startPosition[0];
            startPos[1] = startPosition[1];
        }

        // dfs
        List<List<int[]>> segment = new List<List<int[]>>();
        List<int[]> paths = new List<int[]>();

        map[startPos[0]][startPos[1]] = -1;
        paths.Add(startPos);
        List<int[]> pathsResult = new List<int[]>();

        while (paths.Count > 0)
        {
            int[] node;

            node = paths[paths.Count - 1];
            paths.RemoveAt(paths.Count - 1);

            pathsResult.Add(node);
            if (map[node[0]][node[1]] == 4 || map[node[0]][node[1]] == 5)
            {
                break;
            };

            map[node[0]][node[1]] = -1;

            for (int i = 0; i < 4; i++)
            {

                int[] nextPos = { node[0] + dirY[i], node[1] + dirX[i] };
                if (nextPos[0] > m ||
                    nextPos[0] < 0 ||
                    nextPos[1] > n ||
                    nextPos[1] < 0 ||
                    map[nextPos[0]][nextPos[1]] == -1) continue;

                if (paths.FirstOrDefault(node => node[0] == nextPos[0] && node[1] == nextPos[1]) == null)
                {
                    paths.Add(nextPos);
                }
            }
        }

        Debug.Log($"pathsResult: {JsonConvert.SerializeObject(pathsResult)}");
        Debug.Log("startPos: " + JsonConvert.SerializeObject(startPos));
        return;
    }
}