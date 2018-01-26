using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[CustomEditor (typeof (MapChunkGenerator))]
public class ChunkGeneratorEditor : Editor {

    public override void OnInspectorGUI()
    {
        MapChunkGenerator chunkGen = (MapChunkGenerator)target;

        if (DrawDefaultInspector())
        {
            if (chunkGen.autoUpdate)
            {
                chunkGen.GenerateChunk();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            chunkGen.GenerateChunk();
        }
        if (GUILayout.Button("Clear"))
        {
            chunkGen.ClearChunk();
        }
    }
}
