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
                int[] currentPos = { gridPosition.y, gridPosition.x };
                if (currentPos[0] >= 0 && currentPos[0] < 10 && currentPos[1] >= 0 && currentPos[1] < 10)
                {
                    tilemapLoader.findHallway(GameManager.Instance.currentPosition, currentPos);
                }

            }
        }
    }
}