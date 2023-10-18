using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class DrawPlayerOnMap : MonoBehaviour
{

    // Start is called before the first frame update
    public Tilemap tilemap;
    public TileBase player;
    void Start()
    {
        Vector3Int tilePosition = new Vector3Int(0, 4, 0);
        TileBase tile = player;
        tilemap.SetTile(tilePosition, tile);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
