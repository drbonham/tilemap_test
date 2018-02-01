using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapChunkGeneratorV2 : MonoBehaviour {

    public int seed;
   
    public Vector2Int chunkCnt;
    public Vector2Int chunkSize;
    public LayerSettings[] layerSettings;

    Vector2Int totalSize;
    public MapChunk[] mapChunks;

    // Use this for initialization
    void Awake () {
        totalSize = new Vector2Int(chunkCnt.x * chunkSize.x, chunkCnt.y * chunkSize.y);
        Random.InitState(seed);
        InitializeChunks();
        Generate();
    }

    // Initialize the map chunks and layers
    void InitializeChunks()
    {
        mapChunks = new MapChunk[chunkCnt.x * chunkCnt.y];
        for (int x = 0; x < chunkCnt.x; x++)
        {
            for (int y = 0; y < chunkCnt.y; y++)
            {
                mapChunks[x * chunkCnt.x + y].go = new GameObject("Chunk_" + x + " " + y);
                mapChunks[x * chunkCnt.x + y].go.transform.SetParent(transform);
                mapChunks[x * chunkCnt.x + y].layers = new MapLayer[layerSettings.Length];
                mapChunks[x * chunkCnt.x + y].chunkCoord = new Vector2Int(x, y);
                mapChunks[x * chunkCnt.x + y].IsVisible(false);
                // Generate game object and tilemap/renderer component for each layer in the chunk
                // baesd on layersettings
                for (int i = 0; i < layerSettings.Length; i++)
                {
                    mapChunks[x * chunkCnt.x + y].layers[i].go = new GameObject("Layer_" + layerSettings[i].name);
                    mapChunks[x * chunkCnt.x + y].layers[i].go.transform.SetParent(mapChunks[x * chunkCnt.x + y].go.transform);
                    mapChunks[x * chunkCnt.x + y].layers[i].go.AddComponent<Tilemap>();
                    mapChunks[x * chunkCnt.x + y].layers[i].go.AddComponent<TilemapRenderer>();

                }
            }
        }
    }
    void Generate()
    {
        // Layer
        for (int i = 0; i < layerSettings.Length; i++)
        {
            // For each tile setting in the layer settings, populate the ONE tilemap
            // on the layer game object
            // Later tiles will overwrite previous tiles!
            for (int j = 0; j < layerSettings[i].tileSettings.Length; j++)
            {
                TileSettings curSettings = layerSettings[i].tileSettings[j];
                float[,] noiseMap = NoiseGenerator.GenerateNoiseMap(totalSize.x, totalSize.y, seed, 
                    curSettings.scale, curSettings.octaves, curSettings.persistance, 
                    curSettings.lacunarity, curSettings.offset);

                // Populate the tilemaps for each layer of each chunk
                for (int x = 0; x < chunkCnt.x; x++)
                {
                    for (int y = 0; y < chunkCnt.y; y++)
                    {
                        // Get the tilemap for this layer
                        Tilemap tm = mapChunks[x * chunkCnt.x + y].layers[i].go.GetComponent<Tilemap>();
                        for (int tx = 0; tx < chunkSize.x; tx++)
                        {
                            for (int ty = 0; ty < chunkSize.y; ty++)
                            {
                                // Using only one noisemap per tile type so setting
                                // tile based on the offset here
                                // This should match the world position and noisemap position
                                Vector3Int offset = new Vector3Int(tx + (x*chunkSize.x),ty + (y* chunkSize.y),0);
                                if (noiseMap[offset.x, offset.y] > curSettings.noiseThreshold)
                                {
                                    if (curSettings.noiseMapMode)
                                    {
                                        tm.SetTile(offset, curSettings.tileType);
                                        tm.SetTileFlags(offset, TileFlags.None);
                                        tm.SetColor(offset, Color.Lerp(Color.white, Color.black, noiseMap[offset.x, offset.y]));
                                    }
                                    else if (curSettings.randomizeDensity && (noiseMap[offset.x, offset.y] > Random.value))
                                    {
                                        tm.SetTile(offset, curSettings.tileType);
                                    }
                                    else if (!curSettings.randomizeDensity)
                                    {
                                        tm.SetTile(offset, curSettings.tileType);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}


// The actual game object chunk
public struct MapChunk
{
    public GameObject go;
    public Vector2Int chunkCoord;
    public MapLayer[] layers;

    public void IsVisible(bool visible)
    {
        go.SetActive(visible);
    }

}


// The actual game object layer
public struct MapLayer
{
    public GameObject go;
}


// The settings for each layer/tile
[System.Serializable]
public struct LayerSettings
{
    public string name;
    public TileSettings[] tileSettings;
}

[System.Serializable]
public struct TileSettings
{
    public string name;
    public Tile tileType;
    public bool noiseMapMode;
    public bool randomizeDensity;
    [Range(0,1)]
    public float noiseThreshold;

    public float scale;
    [Range(0,30)]
    public int octaves;
    [Range(0f,1f)]
    public float persistance;
    [Range(1f, 3.2f)]
    public float lacunarity;
    public Vector2 offset;
}

