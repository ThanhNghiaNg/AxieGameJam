using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class DrawPlayerOnMap : MonoBehaviour
{

    // Start is called before the first frame update
    public Tilemap tilemap;
    public TileBase player;
    TileBase tile;
    void Start()
    {
        int[] startPos = GameManager.Instance.startPosition;
        Vector3Int tilePosition = new Vector3Int(startPos[1], startPos[0], 0);
        tile = player;
        tilemap.SetTile(tilePosition, tile);
    }

    // Update is called once per frame
    void Update()
    {
        int[] currentPos = GameManager.Instance.currentPosition;
        Vector3Int tilePosition = new Vector3Int(currentPos[1], currentPos[0], 0);
        tile = player;
        tilemap.ClearAllTiles();
        tilemap.SetTile(tilePosition, tile);
    }
}
