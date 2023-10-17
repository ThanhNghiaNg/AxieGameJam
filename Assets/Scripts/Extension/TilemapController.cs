using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tileEmpty;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        Vector3Int tilePosition = new Vector3Int(0, 0, 0);  // Set the position where you want to paint the tile
        TileBase tile = tileEmpty;  // Reference to the tile you want to paint
        tilemap.SetTile(tilePosition, tile);
    }
}