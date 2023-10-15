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
    private int transformX = 0;
    private int transformY = 0;
    void  Awake(){
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

            Debug.Log("TransformX: " + transformX.ToString());
            Debug.Log("TransformY: " + transformY.ToString());
            Debug.Log("map: " + JsonConvert.SerializeObject(map));
            
            // Debug.Log("tilemapData: \n"+JsonConvert.SerializeObject(sortedTiles));
            // Debug.Log("tilemapData.tiles: \n"+JsonConvert.SerializeObject(tilemapData.tiles));
        }
        else
        {
            Debug.LogError("JSON file not found.");
        }
    }
    public List<List<int>> TilemapToMap(List<TileData> sortedTiles){
        
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
        if (minX < 0) {
            transformX = minX;
            for (int i = 0; i < sortedTiles.Count; i++) sortedTiles[i].x -= minX;
        };
        if (minY < 0) {
            transformY = minY;
            for (int i = 0; i < sortedTiles.Count; i++) sortedTiles[i].y -= minY;
        };

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
        for (int i = 0; i < m; i++){
            List<int> row = new List<int>();
            for (int j = 0; j < n; j++){
                TileData result = sortedTiles.FirstOrDefault(tile => tile.x == j && tile.y == i);
                if (result != null)
                {
                    switch(result.tileName) 
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
    public void MapToHallways(List<List<int>> map){
        int[] dirX = {0 , 0, -1, 1}; // Down - Up - Left - Right
        int[] dirY = {-1 , 1, 0, 0}; // Down - Up - Left - Right
        int[] startPos = {0,0};
        int m = map.Count;
        int n = map[0].Count;
        for (int row = 0; row < m; row++)
        {
            for (int col = 0; col < n; col++)
            {
                if (map[row][col] == 1){
                    startPos[0] = row;
                    startPos[1] = col;
                    break;
                }
            }
        }
        Debug.Log("startPos: "+JsonConvert.SerializeObject(startPos));
        return;
    }
}
