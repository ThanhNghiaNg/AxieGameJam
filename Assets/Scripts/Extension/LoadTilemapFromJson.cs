using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.SceneManagement;

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
    void  Awake(){
        createTilemap();
        LoadTilemapFromJsonFile(Application.persistentDataPath + "/" + tilemap.name.ToLower() + ".json");
    }
    public void createTilemap()
    {
        tilemap = new GameObject("Tilemap").AddComponent<Tilemap>();
        tilemap.name = this.gameObject.name;//SceneManager.GetActiveScene().name;
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
            Debug.Log("tilemapData: \n"+JsonConvert.SerializeObject(tilemapData));
        }
        else
        {
            Debug.LogError("JSON file not found.");
        }
    }
}
