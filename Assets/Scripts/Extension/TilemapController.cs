using UnityEngine;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

public class TilemapController : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tileEmpty;
    private LoadTilemapFromJson tilemapLoader;

    void Awake()
    {
        // tilemap = GetComponent<Tilemap>();

        // DrawTilemap();
        // LoadTilemapFromJson.Instance.Justlog();
        tilemapLoader = GetComponent<LoadTilemapFromJson>();

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);

            // Check if the clicked position contains the specific tile.
            if (tilemap.GetTile(gridPosition) != tileEmpty)
            {
                // tilemapLoader.findCurrentHallway()
                // Perform your action when the specific tile is clicked.
                // Debug.Log($"tilemapLoader: {}");
                // Debug.Log("Position of clicked tile: " + gridPosition);
                // Debug.Log("Do something");
                int[] currentPos = { gridPosition.y, gridPosition.x };
                if (currentPos[0] >= 0 && currentPos[0] < 10 && currentPos[1] >= 0 && currentPos[1] < 10)
                {
                    Debug.Log($"currentPos {JsonConvert.SerializeObject(currentPos)}");
                    tilemapLoader.findCurrentHallway(currentPos);
                }

            }
        }
    }
}