using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



public class TileMapChunkGenerator : MonoBehaviour {

    public static TileMapChunkGenerator _Instance;

    public Vector2Int chunkCnt;
    public Vector2Int chunkSize;
    public float noiseScale;
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    [Range(1, 3.2f)]
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public TileMapLayer[] tilemapLayers;
    [HideInInspector]
    public TileMapChunk[] tilemapChunks;

    float[,] noiseMap;

    public void Awake()
    {
        Iinitialize();
    }

    public void Iinitialize() {
        if (_Instance == null)
            _Instance = this;

        noiseMap = NoiseGenerator.GenerateNoiseMap(chunkSize.x*chunkCnt.x, chunkSize.y*chunkCnt.y, seed, noiseScale, octaves, 
            persistance,lacunarity, offset);

        tilemapChunks = new TileMapChunk[chunkCnt.x * chunkCnt.y];
        GenerateChunks();
    }

    void GenerateChunks()
    {
        for (int x = 0; x < chunkCnt.x; x++)
        {
            for (int y = 0; y < chunkCnt.y; y++)
            {
                GameObject chunk = new GameObject("Chunk_" + x + "_" + y);
                chunk.transform.SetParent(transform);
                TileMapChunk tilemapChunk = new TileMapChunk();
                tilemapChunk.layers = new TileMapLayer[tilemapLayers.Length];

                for (int i = 0; i < tilemapLayers.Length; i++)
                {
                    GameObject layer = new GameObject("Layer_" + tilemapLayers[i].name);
                    layer.transform.SetParent(chunk.transform);

                    tilemapChunk.layers[i] 
                        = new TileMapLayer(tilemapLayers[i].name, tilemapLayers[i].tile, tilemapLayers[i].randomizeDensity, tilemapLayers[i].noiseThreshold, null);
                    Tilemap tm = layer.AddComponent<Tilemap>();
                    layer.AddComponent<TilemapRenderer>();

                    for (int sx = 0; sx < chunkSize.x; sx++)
                    {
                        for (int sy = 0; sy < chunkSize.y; sy++)
                        {
                            Vector3Int chunkOffset = new Vector3Int(sx + (x * chunkSize.x),
                                sy + (y * chunkSize.y),
                                0);
                            if (tilemapLayers[i].randomizeDensity && (noiseMap[chunkOffset.x, chunkOffset.y] > Random.value)
                                && (noiseMap[chunkOffset.x, chunkOffset.y] > tilemapLayers[i].noiseThreshold))
                            {
                                tm.SetTile(chunkOffset, tilemapLayers[i].tile);
                            }
                            else if (!tilemapLayers[i].randomizeDensity)
                            {
                                tm.SetTile(new Vector3Int(sx + (x * chunkSize.x), sy + (y * chunkSize.y), 0), tilemapLayers[i].tile);
                            }
                        }
                    }
                    tilemapChunk.layers[i].tilemap = tm;
                }

                tilemapChunks[chunkCnt.x * x + y] = tilemapChunk;
            }
        }
    }

	// This is assuming worldspace/sprite sizes and noisemap are 1 to 1
    // Building from 0,0 up, can change the maths later to center 
    /*void GenerateChunks()
    {
        for (int i = 0; i < tilemapLayers.Length; i++)
        {
            // Keep track of the chunks for this layer
            Tilemap[] layerChunks = new Tilemap[chunkCnt.x * chunkCnt.y];

            for (int x = 0; x < chunkCnt.x; x++)
            {
                for (int y = 0; y < chunkCnt.y; y++)
                {
                    GameObject chunk = new GameObject(tilemapLayers[i].name + x + "_" + y);
                    chunk.transform.SetParent(transform);

                    Tilemap tmp = chunk.AddComponent<Tilemap>();
                    chunk.AddComponent<TilemapRenderer>();

                    if(tilemapLayers[i].generateCollider)
                        chunk.AddComponent<TilemapCollider2D>();
                  

                    for (int sx = 0; sx < chunkSize.x; sx++)
                    {
                        for (int sy = 0; sy < chunkSize.y; sy++)
                        {
                            Vector3Int chunkOffset = new Vector3Int(sx + (x * chunkSize.x), 
                                sy + (y * chunkSize.y), 
                                0);
                            if (tilemapLayers[i].randomizeDensity && (noiseMap[chunkOffset.x, chunkOffset.y] > Random.value)
                                && (noiseMap[chunkOffset.x, chunkOffset.y] > tilemapLayers[i].noiseThreshold))
                            {
                                tmp.SetTile(chunkOffset, tilemapLayers[i].tile);
                            }
                            else if (!tilemapLayers[i].randomizeDensity)
                            {
                                tmp.SetTile(new Vector3Int(sx + (x * chunkSize.x), sy + (y * chunkSize.y), 0), tilemapLayers[i].tile);
                                // This will draw the noisemap instead of filling 
                                //    tmp.SetTile(chunkOffset, layer.tile);
                                //    tmp.SetTileFlags(chunkOffset, TileFlags.None);
                                //    tmp.SetColor(chunkOffset,
                                //        Color.Lerp(Color.white, Color.black, noiseMap[chunkOffset.x, chunkOffset.y]));
                            }
                        }
                    }
                    layerChunks[chunkCnt.x * x + y] = tmp;
                }
            }

            tilemapLayers[i].tilemapChunks = layerChunks;
        }
    }*/
}

[System.Serializable]
public struct TileMapChunk
{
    public string name;
    public TileMapLayer[] layers;
     
}

[System.Serializable]
public struct TileMapLayer
{
    public string name;
    public Tile tile;
    public bool randomizeDensity;
    public float noiseThreshold;
    //public NoiseMapSettings noiseMapSettings;
    [HideInInspector]
    public Tilemap tilemap;

    public TileMapLayer(string name, Tile tile, bool randomizeDensity, float noiseThreshold, Tilemap tilemap)
    {
        this.name = name;
        this.tile = tile;
        this.randomizeDensity = randomizeDensity;
        this.noiseThreshold = noiseThreshold;
        this.tilemap = tilemap;
        //this.noiseMapSettings = noiseMapSettings;
    }
}

[System.Serializable]
public struct NoiseMapSettings
{
    public int seed;
    public float noiseScale;
    public int ocataves;
    public float persistance;
    public float lacunarity;
    public float offset;
}

//[System.Serializable]
//public struct TileMapLayer
//{
//    public string name;
//    public Tile tile;
//    public bool randomizeDensity;
//    public float noiseThreshold;
//    public bool generateCollider;
//    [HideInInspector]
//    public Tilemap[] tilemapChunks;


//    public TileMapLayer(string name, Tile tile, bool randomizeDensity, float noiseThreshold, bool generateCollider, Tilemap[] tilemapChunks)
//    {
//        this.name = name;
//        this.tile = tile;
//        this.randomizeDensity = randomizeDensity;
//        this.noiseThreshold = noiseThreshold;
//        this.generateCollider = generateCollider;
//        this.tilemapChunks = tilemapChunks;
//    }
//}

//public struct TileMapChunk
//{
//    Tilemap chunk;
//    Vector2 worldPos;
//}
