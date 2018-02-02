using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 5f;
    float xMin, xMax, yMin, yMax;
    Rigidbody2D rigidBody;
    float moveHorizontal, moveVertical;
    Vector3 moveDirection;
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
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveHorizontal, moveVertical, 0);
    }

    private void FixedUpdate()
    {
        //rigidBody.velocity = Vector3.zero;
        rigidBody.velocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
    }
}
