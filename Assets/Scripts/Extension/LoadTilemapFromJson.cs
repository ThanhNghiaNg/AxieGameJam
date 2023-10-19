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
    public static LoadTilemapFromJson Instance { get; private set; }
    public Tilemap tilemap;
    public Tile[] tilePrefabs;
    public TileBase tileEmpty;
    public TileBase tileRoom;
    public TileBase tileBoss;
    public TileBase tileStart;
    public TileBase tileWall;
    public List<List<int>> map = new List<List<int>>();
    public List<int[]> segments;
    private int normalizeDistance = 0;
    private int mapCol = 10;
    private int mapRow = 10;
    private int[] dirX = { 0, 0, -1, 1 }; // Down - Up - Left - Right
    private int[] dirY = { -1, 1, 0, 0 }; // Down - Up - Left - Right
    void Awake()
    {
        // createTilemap();
        LoadTilemapFromJsonFile(Application.persistentDataPath + "/" + tilemap.name.ToLower() + ".json");
        DrawTilemap();
        int[] startPos = { 0, 0 };
        // find start position;
        for (int row = 0; row < map.Count; row++)
        {
            for (int col = 0; col < map[0].Count; col++)
            {
                if (map[row][col] == 1)
                {
                    startPos[0] = row;
                    startPos[1] = col;
                    break;
                }
            }
        }
        findEndPosition(startPos);
        // LoadTilemapFromJsonFile(Application.persistentDataPath + "/" + "level2" + ".json");
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
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
            string json = File.ReadAllText(filePath);
            TilemapData tilemapData = JsonConvert.DeserializeObject<TilemapData>(json);

            List<TileData> sortedTiles = tilemapData.tiles.OrderBy(tile => tile.x).ThenBy(tile => tile.y).ToList();
            // sortedTiles = FlipHorizotalTilemap(sortedTiles);
            // sortedTiles = FlipHorizotalTilemap(sortedTiles);
            map = TilemapToMap(sortedTiles);
            // int[] pos = null; //{ 2, 2 };
            // findCurrentHallway(map, pos);
        }
        else
        {
            Debug.LogError("JSON file not found.");
        }
    }
    public void DrawTilemap()
    {
        Dictionary<int, TileBase> mapToTile = new Dictionary<int, TileBase>();
        mapToTile[-1] = tileWall;
        mapToTile[0] = tileEmpty;
        mapToTile[1] = tileStart;
        mapToTile[4] = tileRoom;
        mapToTile[5] = tileBoss;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector3Int tilePosition = new Vector3Int(i, j, 0);
                TileBase tile = mapToTile[map[j][i]];
                tilemap.SetTile(tilePosition, tile);
            }
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
        for (int i = 0; i < tilemap.Count; i++)
        {
            tilemap[i].y -= 2 * Math.Abs(tilemap[i].y - minY);
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
        for (int i = 0; i < m; i++)
        {
            List<int> row = new List<int>();
            for (int j = 0; j < n; j++)
            {
                TileData result = tilemap.FirstOrDefault(tile => tile.x == j && tile.y == i);
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
    public void findEndPosition(int[]? startPosition)
    {
        List<List<int>> tempMap = new List<List<int>>();
        segments = new List<int[]>();
        for (int i = 0; i < map.Count; i++)
        {
            List<int> row = new List<int>();
            for (int j = 0; j < map[0].Count; j++)
            {
                row.Add(map[i][j]);
                // tempMap[i][j] = map[i][j];
            }
            tempMap.Add(row);
        }

        List<int[]> listNode = new List<int[]>();
        int[] firstNode = { startPosition[0], startPosition[1] };
        int[] endPosition = { 0, 0 };
        listNode.Add(firstNode);
        while (listNode.Count > 0)
        {
            int[] currentNode = listNode[listNode.Count - 1];
            listNode.RemoveAt(listNode.Count - 1);
            
            segments.Add(currentNode);
            
            if (
                (tempMap[currentNode[0]][currentNode[1]] == 4 || tempMap[currentNode[0]][currentNode[1]] == 5)
                && (currentNode[0] != startPosition[1] && currentNode[1] != startPosition[0]))
            {
                endPosition = currentNode;
                break;
            }
            
            tempMap[currentNode[0]][currentNode[1]] = -1;

            for (int i = 0; i < 4; i++)
            {
                int[] nextPos = { currentNode[0] + dirY[i], currentNode[1] + dirX[i] };
                if (nextPos[0] > mapRow ||
                    nextPos[0] < 0 ||
                    nextPos[1] > mapCol ||
                    nextPos[1] < 0 ||
                    tempMap[nextPos[0]][nextPos[1]] == -1) continue;

                if (listNode.FirstOrDefault(node => node[0] == nextPos[0] && node[1] == nextPos[1]) == null)
                {
                    listNode.Add(nextPos);
                }
            }

        }
        GameManager.Instance.SetInitPositions(startPosition, endPosition);
    }
    public void findCurrentHallway(int[]? startPosition)
    {

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
        return;
    }
}
