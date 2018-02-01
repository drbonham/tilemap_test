using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.Tilemaps;

public class TreeTile : Tile {

    // Use this for initialization
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        go.GetComponent<SpriteRenderer>().sortingOrder = -position.y * 2;
        return base.StartUp(position, tilemap, go);
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/TreeTile")]
    public static void CreateTreeTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save treetile", "TreeTile", "asset", "Save treetile", "Assets");
        if (path =="")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<TreeTile>(), path);
    }
#endif
}
