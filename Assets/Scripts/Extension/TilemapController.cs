using UnityEngine;
using UnityEngine.Tilemaps;
using Newtonsoft.Json;

public class TilemapController : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tileEmpty;

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();

        DrawTilemap();
    }
    public void DrawTilemap()
    {
        // Vector3Int tilePosition = new Vector3Int(0, 0, 0);  // Set the position where you want to paint the tile
        // TileBase tile = tileEmpty;  // Reference to the tile you want to paint
        // tilemap.SetTile(tilePosition, tile);
        // tilemap.SetTile(new Vector3Int(0, 1, 0), tile);
        // Debug.Log($"map--: {JsonConvert.SerializeObject(LoadTilemapFromJson.Instance.map)}");
        Debug.Log($"map--: {LoadTilemapFromJson.Instance.map}");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);

            // Check if the clicked position contains the specific tile.
            if (tilemap.GetTile(gridPosition) == tileEmpty)
            {
                // Perform your action when the specific tile is clicked.
                Debug.Log("Position of clicked tile: " + gridPosition);
                Debug.Log("Do something");
            }
        }
    }
}