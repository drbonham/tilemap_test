using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Tilemaps;

public class RiverTile : Tile
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
    [MenuItem("Assets/Create/Tiles/RiverTile")]
    public static void CreateRiverTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save rivertile", "RiverTile", "asset", "Save rivertile", "Assets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<RiverTile>(), path);
    }
#endif
}
