using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Tilemaps;

public class DirtTile : Tile
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/DirtTile")]
    public static void CreateDirtTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save dirttile", "DirtTile", "asset", "Save dirttile", "Assets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DirtTile>(), path);
    }
#endif
}