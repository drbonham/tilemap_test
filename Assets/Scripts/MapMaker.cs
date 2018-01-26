using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapMaker : MonoBehaviour {

    public int chunkWidth;
    public int chunkHeight;
    public int seed;
    public float noiseScale;
    public int octaves;
    [Range(0, 1)]
    public float persistance;
    [Range(1, 3.2f)]
    public float lacunarity;
    public Vector2 offset;
    public bool randomizeDensity;
    [Range(0, 1)]
    public float threshold;

    public Vector3Int chunkPosition;

    [SerializeField]
    static int numberOfTiles;
    public Tilemap tilemap;
    public Tile[] tile = new Tile[numberOfTiles];

    void Update()
    {
        Vector3Int newPosition = new Vector3Int(100, 0, 0);
        Vector2 newOffset = new Vector2(100, 0);
        Vector3Int newPosition2 = new Vector3Int(100, 100, 0);
        Vector2 newOffset2 = new Vector2(100, 100);
        GenerateChunk(tilemap, tile[1], chunkWidth, chunkHeight, chunkPosition, seed, noiseScale, octaves, persistance, lacunarity, offset, randomizeDensity, threshold);
        GenerateChunk(tilemap, tile[2], chunkWidth, chunkHeight, newPosition, seed, noiseScale, octaves, persistance, lacunarity, newOffset, randomizeDensity, threshold);
        GenerateChunk(tilemap, tile[2], chunkWidth, chunkHeight, newPosition2, seed, noiseScale, octaves, persistance, lacunarity, newOffset2, randomizeDensity, threshold);
    }

    void GenerateChunk(Tilemap _Tilemap, Tile _Tile, int chunkWidth, int chunkHeight, Vector3Int chunkPosition, int seed, float noiseScale, int octaves, float persistance, float lacunarity, Vector2 offset, bool randomizeDensity, float threshold)
    //public void GenerateChunk( float threshold, int seed)
    {

        float[,] noiseMap = NoiseGenerator.GenerateNoiseMap(chunkWidth, chunkHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        //ClearChunk(_Tilemap);
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Random.InitState(seed);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int tilePos = new Vector3Int(-x + chunkPosition.x + width / 2, -y + chunkPosition.y + height / 2, 0);
                
                if (_Tilemap.name == "Noise")
                {
                    _Tilemap.SetTile(tilePos, _Tile);
                    _Tilemap.SetTileFlags(tilePos, TileFlags.None);
                    _Tilemap.SetColor(tilePos, Color.Lerp(Color.white, Color.black, noiseMap[x, y]));
                }
                else if (noiseMap[x, y] > threshold)
                {
                    if (randomizeDensity && noiseMap[x, y] > Random.value)
                    {
                        _Tilemap.SetTile(tilePos, _Tile);
                    }
                    else if (!randomizeDensity)
                    {
                        _Tilemap.SetTile(tilePos, _Tile);
                    }
                }
            }
        }
    }

    public void ClearChunk(Tilemap _Tilemap)
    {
        _Tilemap.ClearAllTiles();
    }


}
