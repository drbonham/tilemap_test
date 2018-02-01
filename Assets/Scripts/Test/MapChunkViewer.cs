using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChunkViewer : MonoBehaviour {


    public int maxViewDistance;
    Vector2Int chunkSize;
    Vector2Int chunkCnt;

    Transform viewer;
    Vector2 viewerPosition;

    Vector2Int currentChunkPosition;
    Vector2Int lastChunkPosition;
    MapChunk[] mapChunks;


	// Use this for initialization
	void Start () {
        viewer = transform;
        chunkSize = FindObjectOfType<TileMapChunkGeneratorV2>().chunkSize;
        chunkCnt = FindObjectOfType<TileMapChunkGeneratorV2>().chunkCnt;
        mapChunks = FindObjectOfType<TileMapChunkGeneratorV2>().mapChunks;
        currentChunkPosition = new Vector2Int(Mathf.FloorToInt(viewerPosition.x / chunkSize.x), Mathf.FloorToInt(viewerPosition.y / chunkSize.y));
        lastChunkPosition = currentChunkPosition;
        UpdateChunksVisible();
    }
	
	// Update is called once per frame
	void Update () {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.y);
        currentChunkPosition = new Vector2Int(Mathf.FloorToInt(viewerPosition.x / chunkSize.x), Mathf.FloorToInt(viewerPosition.y / chunkSize.y));

        // Update chunks if viewer has moved to a new chunk
        if (Vector2.Distance(currentChunkPosition, lastChunkPosition) >= 1)
        {
            UpdateChunksVisible();
        }
        print(currentChunkPosition);
        print(lastChunkPosition);
    }

    void UpdateChunksVisible()
    {
        for (int x = currentChunkPosition.x - maxViewDistance; x <= currentChunkPosition.x + maxViewDistance; x++)
        {
            for (int y = currentChunkPosition.y - maxViewDistance; y <= currentChunkPosition.y + maxViewDistance; y++)
            {
                if ((x >= 0 && y >= 0) && (x < chunkCnt.x && y < chunkCnt.y))
                {
                    mapChunks[x * chunkCnt.x + y].IsVisible(true);

                    if (Mathf.Abs(currentChunkPosition.x - lastChunkPosition.x) > 0)
                    {
                        int newX = (lastChunkPosition.x - (currentChunkPosition.x - lastChunkPosition.x) * maxViewDistance);
                        if (newX >= 0)
                        {
                            mapChunks[newX * chunkCnt.x + y].IsVisible(false);
                        }
                    }
                    if (Mathf.Abs(currentChunkPosition.y - lastChunkPosition.y) > 0)
                    {
                        int newY = (lastChunkPosition.y - (currentChunkPosition.y - lastChunkPosition.y) * maxViewDistance);
                        if (newY >= 0)
                        {
                            mapChunks[x * chunkCnt.x + newY].IsVisible(false);
                        }
                    }
                }
            }
        }
        lastChunkPosition = currentChunkPosition;
    }
}
