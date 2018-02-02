using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ObstacleTile : Tile
{

    // Use this for initialization
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        go.GetComponent<SpriteRenderer>().sortingOrder = -position.y * 3;
        return base.StartUp(position, tilemap, go);
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/ObstacleTile")]
    public static void CreateTreeTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save obstacletile", "ObstacleTile", "asset", "Save ObstacleTile", "Assets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<ObstacleTile>(), path);
    }
#endif
}
