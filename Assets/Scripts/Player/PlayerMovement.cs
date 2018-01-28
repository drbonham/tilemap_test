using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 5f;
    float xMin, xMax, yMin, yMax;
	// Use this for initialization
	void Start () {
        if(TileMapChunkGenerator._Instance != null)
        {
            Tilemap tilemapChunkMin = TileMapChunkGenerator._Instance.tilemapChunks[0].layers[0].tilemap;
            Tilemap tilemapChunkMax = TileMapChunkGenerator._Instance.tilemapChunks[TileMapChunkGenerator._Instance.tilemapChunks.Length - 1].layers[0].tilemap; Vector3 minTile = tilemapChunkMin.CellToWorld(tilemapChunkMin.cellBounds.min);
            Vector3 maxTile = tilemapChunkMin.CellToWorld(tilemapChunkMax.cellBounds.max);
            xMin = minTile.x;
            xMax = maxTile.x;
            yMin = minTile.y;
            yMax = maxTile.y;
        }
        else
        {
            xMin = yMin = 0;
            xMax = yMax = 3000;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A) && transform.position.x > xMin)
        {
            transform.position 
                = new Vector3(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < xMax)
        {
            transform.position
                = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.W) && transform.position.y < yMax)
        {
            transform.position
                = new Vector3(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime, transform.position.z);
        }
        if (Input.GetKey(KeyCode.S) && transform.position.y > yMin)
        {
            transform.position
                = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);
        }
    }
}
