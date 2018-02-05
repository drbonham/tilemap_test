using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerCameraFollow : MonoBehaviour {

    Transform target;

    float xMax, xMin, yMax, yMin;
    // Camera
    public float _minZoom = 1f;
    public float _maxZoom = 20f;

    void Start()
    {
        target = transform;
        if (TileMapChunkGeneratorV2._Instance != null)
        {
            Vector3[] bounds = TileMapChunkGeneratorV2._Instance.MapBounds();
            SetLimits(bounds[0], bounds[1]);
        }
        else
        {
            xMin = yMin = 0;
            xMax = yMax = 3000;
        }
    }

    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(Mathf.Clamp(target.position.x,xMin,xMax),
            Mathf.Clamp(target.position.y,yMin,yMax), -10);
        Camera.main.transform.position = targetPos;

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, _minZoom, _maxZoom);
    }

    void SetLimits(Vector3 minTile, Vector3 maxTile)
    {
        Camera cam = Camera.main;
        float h = 2f * cam.orthographicSize;
        float w = h * cam.aspect;

        xMin = minTile.x + w / 2;
        xMax = maxTile.x - w / 2;
        yMin = minTile.y + h / 2;
        yMax = maxTile.y - h / 2;
    }
}