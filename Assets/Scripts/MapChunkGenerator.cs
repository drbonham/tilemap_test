using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapChunkGenerator : MonoBehaviour {

    
    public int chunkWidth;
    public int chunkHeight;
    public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    [Range(1,3.2f)]
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;
    public bool randomizeDensity;

    [Range(0,1)]
    public float threshold;

    void OnValidate()
    {
        if (chunkWidth < 1)
        {
            chunkWidth = 1;
        }
        if (chunkHeight < 1)
        {
            chunkHeight = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }

    //public void GenerateChunk(int chunkWidth, int chunkHeight, int seed, float noiseScale, int octaves, float persistance, float lacunarity, Vector2 offset)
    public void GenerateChunk()
    {
        OnValidate();
        float[,] noiseMap = NoiseGenerator.GenerateNoiseMap(chunkWidth,chunkHeight,seed,noiseScale,octaves,persistance,lacunarity,offset);
        DrawTiles(noiseMap,threshold,seed);
    }

    public void ClearChunk()
    {
        _Tilemap.ClearAllTiles();
    }

    public Tilemap _Tilemap;
    public Tile _Tile;

    public void DrawTiles(float[,] noiseMap,float threshold,int seed)
    {
        ClearChunk();
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Random.InitState(seed);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int tilePos = new Vector3Int(-x + width / 2, -y + height / 2, 0);

                if (_Tilemap.name == "Noise")
                {
                    _Tilemap.SetTile(tilePos, _Tile);
                    _Tilemap.SetTileFlags(tilePos, TileFlags.None);
                    _Tilemap.SetColor(tilePos, Color.Lerp(Color.white, Color.black, noiseMap[x, y]));
                } else if (noiseMap[x, y] > threshold)
                {
                    if (randomizeDensity && noiseMap[x, y] > Random.value)
                    {
                        _Tilemap.SetTile(tilePos, _Tile);
                    } else if (!randomizeDensity)
                    {
                        _Tilemap.SetTile(tilePos, _Tile);
                    }
                } 
            }
        }

    }


}
