using UnityEngine;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

public class TilemapController : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tileEmpty;
    private MapManager tilemapLoader;

    void Awake()
    {
        tilemapLoader = GetComponent<MapManager>();

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
                int[] currentPos = { gridPosition.y, gridPosition.x };
                if (currentPos[0] >= 0 && currentPos[0] < 10 && currentPos[1] >= 0 && currentPos[1] < 10)
                {
                    MapManager.Instance.findHallway(GameManager.Instance.currentPosition, currentPos);
                }
            }
        }
    }
}