using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

// [System.Serializable]
// public class TileData
// {
//     public int x;
//     public int y;
//     public string tileName;
// }

// // [System.Serializable]
// public class TilemapData
// {
//     public List<TileData> tiles;
// }

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

            List<List<int>> map = TilemapToMap(sortedTiles);
            MapToHallways(map);

            Debug.Log("normalizeDistance: " + normalizeDistance.ToString());
            Debug.Log("map: " + JsonConvert.SerializeObject(map));

            // Debug.Log("tilemapData: \n"+JsonConvert.SerializeObject(sortedTiles));
            // Debug.Log("tilemapData.tiles: \n"+JsonConvert.SerializeObject(tilemapData.tiles));
        }
        else
        {
            Debug.LogError("JSON file not found.");
        }
    }
    public List<List<int>> TilemapToMap(List<TileData> sortedTiles)
    {

        int minX = 1000000, minY = 1000000;
        int maxX = -1000000, maxY = -100000;
        for (int i = 0; i < sortedTiles.Count; i++)
        {
            TileData tileData = sortedTiles[i];
            if (tileData.x > maxX) maxX = tileData.x;
            if (tileData.x < minX) minX = tileData.x;
            if (tileData.y > maxY) maxY = tileData.y;
            if (tileData.y < minY) minY = tileData.y;
        }
        // normalize map
        normalizeDistance = Math.Min(minX, minY);
        if (normalizeDistance < 0)
        {
            for (int i = 0; i < sortedTiles.Count; i++) sortedTiles[i].x -= normalizeDistance;
            for (int i = 0; i < sortedTiles.Count; i++) sortedTiles[i].y -= normalizeDistance;
        }


        // find size of map
        minX = 1000000; minY = 1000000;
        maxX = -1000000; maxY = -100000;
        for (int i = 0; i < sortedTiles.Count; i++)
        {
            TileData tileData = sortedTiles[i];
            if (tileData.x > maxX) maxX = tileData.x;
            if (tileData.x < minX) minX = tileData.x;
            if (tileData.y > maxY) maxY = tileData.y;
            if (tileData.y < minY) minY = tileData.y;
        }
        int n = maxX - minX + 1;
        int m = maxY - minY + 1;

        // generate map
        List<List<int>> map = new List<List<int>>();
        for (int i = 0; i <= m; i++)
        {
            List<int> row = new List<int>();
            for (int j = 0; j <= n; j++)
            {
                TileData result = sortedTiles.FirstOrDefault(tile => tile.x - minX == j && tile.y - minY == i);
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
    public void MapToHallways(List<List<int>> map)
    {
        int[] dirX = { 0, 0, -1, 1 }; // Down - Up - Left - Right
        int[] dirY = { -1, 1, 0, 0 }; // Down - Up - Left - Right
        int[] startPos = { 0, 0 };
        int m = map.Count;
        int n = map[0].Count;
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

        // dfs
        List<List<int[]>> segment = new List<List<int[]>>();
        List<int[]> paths = new List<int[]>();
        // Debug.Log($"map: {JsonConvert.SerializeObject(map)}");
        map[startPos[0]][startPos[1]] = -1;
        paths.Add(startPos);
        List<int[]> pathsResult = new List<int[]>();
        bool isBFS = false;
        while (paths.Count > 0)
        {
            int[] node;
            if (isBFS)
            {
                node = paths[0];
                paths.RemoveAt(0);
            }
            else
            {
                node = paths[paths.Count - 1];
                paths.RemoveAt(paths.Count - 1);
            }

            // if (map[node[0]][node[1]] == 5)
            // {
            //     isBFS = true;
            // };
            isBFS = false;

            pathsResult.Add(node);
            if (map[node[0]][node[1]] == 4 || map[node[0]][node[1]] == 5)
            {
                // pathsResult.Add(nextPos);
                if (paths.Count >= 1) isBFS = true;
                List<int[]> clonedList = new List<int[]>(pathsResult.Count);
                foreach (int[] array in pathsResult)
                {
                    int[] clonedArray = new int[array.Length];
                    Array.Copy(array, clonedArray, array.Length);
                    clonedList.Add(clonedArray);
                }
                segment.Add(clonedList);
                pathsResult.Clear();
            };

            map[node[0]][node[1]] = -1;

            // if (node[0] == m - 1 && node[1] == n - 1)
            // {
            //     return;
            //     // return node[2]
            // }

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
            Debug.Log($"paths: {JsonConvert.SerializeObject(paths)}");
            Debug.Log("\n");
        }

        // List<int[]> clonedListAfter = new List<int[]>(pathsResult.Count);
        // foreach (int[] array in pathsResult)
        // {
        //     int[] clonedArray = new int[array.Length];
        //     Array.Copy(array, clonedArray, array.Length);
        //     clonedListAfter.Add(clonedArray);
        // }
        // segment.Add(clonedListAfter);

        Debug.Log($"segment: {JsonConvert.SerializeObject(segment)}");
        Debug.Log($"pathsResult: {JsonConvert.SerializeObject(pathsResult)}");
        Debug.Log("startPos: " + JsonConvert.SerializeObject(startPos));
        return;
    }
}
