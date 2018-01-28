using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapChunkDraw : MonoBehaviour {

    Vector3 mouseToWorld;

    private void Start()
    {
        //CheckLayerChunks();
    }
    void Update()
    {
        if (TileMapChunkGenerator._Instance == null)
            return;

        mouseToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseToWorld.z = 0;
    }

    void CheckLayerChunks()
    {
        TileMapChunkGenerator t = TileMapChunkGenerator._Instance;
        for (int i = 0; i < t.tilemapLayers.Length; i++)
        {
            //for (int j = 0; j < t.tilemapLayers[i].tilemapChunks.Length; j++)
            //{
            //    Debug.Log(t.tilemapLayers[i].tilemapChunks[j].CellToWorld(new Vector3Int(0,0,0)));
            //}
        }
    }
}
