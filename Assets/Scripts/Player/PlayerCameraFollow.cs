using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCameraFollow : MonoBehaviour {

    Transform target;
    Tilemap tilemapChunkMin;
    Tilemap tilemapChunkMax;

    float xMax, xMin, yMax, yMin;

    void Start()
    {
        target = transform;
        if (TileMapChunkGenerator._Instance != null)
        {
            Tilemap tilemapChunkMin = TileMapChunkGenerator._Instance.tilemapChunks[0].layers[0].tilemap;
            Tilemap tilemapChunkMax = TileMapChunkGenerator._Instance.tilemapChunks[TileMapChunkGenerator._Instance.tilemapChunks.Length - 1].layers[0].tilemap;
            Vector3 minTile = tilemapChunkMin.CellToWorld(tilemapChunkMin.cellBounds.min);
            Vector3 maxTile = tilemapChunkMin.CellToWorld(tilemapChunkMax.cellBounds.max);
            SetLimits(minTile, maxTile);
        }
        else
        {
            xMin = yMin = 0;
            xMax = yMax = 3000;
        }
    }

    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(Mathf.Clamp(target.position.x,xMin,xMax),
            Mathf.Clamp(target.position.y,yMin,yMax), -10);
        Camera.main.transform.position = targetPos;
    }

    void SetLimits(Vector3 minTile, Vector3 maxTile)
    {
        Camera cam = Camera.main;
        float h = 2f * cam.orthographicSize;
        float w = h * cam.aspect;

        xMin = minTile.x + w / 2;
        xMax = maxTile.x - w / 2;
        yMin = minTile.y + h / 2;
        yMax = maxTile.y - h / 2;
    }
}