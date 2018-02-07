using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PrebuiltPlacer : MonoBehaviour {


    public PrebuiltBuilding[] buildings;
       
    
    // Use this for initialization
	void Start () {

    }

    public static void Placer(PrebuiltBuilding building, Tilemap _tilemap, Vector3Int location)
    {

     //   TileBase[] tileArray = building.tilemap.GetTilesBlock(building.area);
        for (int x = 0; x < building.size.x; x++)
        {
            for (int y = 0; y < building.size.y; y++)
            {
                Vector3Int sourceLocation = new Vector3Int(building.position.x + x, building.position.y + y, 0);
                Vector3Int newLocation = new Vector3Int(location.x + x, location.y + y, 0);
                _tilemap.SetTile(newLocation, building.tilemap.GetTile(sourceLocation));
                print(building.tilemap.GetTile(sourceLocation));
                print(newLocation);
                //_tilemap.SetTile(newLocation,tileArray[x + y * building.area.size.x]);
            }
        }
    }

    [System.Serializable]
    public struct PrebuiltBuilding
    {
        public string name;
        //public BoundsInt area;
        public Vector3Int position;
        public Vector2Int size;
        public Tilemap tilemap;
    } 

}
