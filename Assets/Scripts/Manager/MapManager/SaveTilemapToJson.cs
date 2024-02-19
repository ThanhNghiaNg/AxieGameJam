using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.IO;

// [System.Serializable]
public class TileData
{
    public int x;
    public int y;
    public string tileName;
}

// [System.Serializable]
public class TilemapData
{
    public List<TileData> tiles;
}

public class SaveTilemapToJson : MonoBehaviour
{
    public Tilemap tilemap;
    void Awake(){
        SaveTilemapToJsonFile(Path.Combine(Application.persistentDataPath, tilemap.name + ".json"));
    }
    public void SaveTilemapToJsonFile(string filePath)
    {
        TilemapData tilemapData = new TilemapData();
        tilemapData.tiles = new List<TileData>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                TileData tileData = new TileData();
                tileData.x = pos.x;
                tileData.y = pos.y;
                tileData.tileName = tilemap.GetTile(pos).name;
                tilemapData.tiles.Add(tileData);
            }
        }

        string json = JsonConvert.SerializeObject(tilemapData);
        File.WriteAllText(filePath, json);
    }
}
