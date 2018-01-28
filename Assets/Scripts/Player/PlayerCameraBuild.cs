using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraBuild : MonoBehaviour {

    // Mouse coordinates at start/end of each frame
    Vector3 _curFramePosition;                                                       // Cache current mouse position
    Vector3 _lastFramePosition;                                                      // Cache last frame mouse position

    // Camera
    public float _minZoom = 5f;
    public float _maxZoom = 20f;

    // Update is called once per frame
    void Update()
    {
        updateCameraMovement();
    }

    private void updateCameraMovement()
    {
        _curFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);     // Use main camera to get mouse pos in world coords
        _curFramePosition.z = 0;                                                     // Set mos pos z to 0 to avoid any wierdness
        if (Input.GetMouseButton(2))                     // middle mouse
        {
            Vector3 diff = _lastFramePosition - _curFramePosition;                    // Update camera to mouse movement difference
            Camera.main.transform.Translate(diff);
        }

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, _minZoom, _maxZoom);
        _lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // Use main camera to get mouse pos in world coords
        _lastFramePosition.z = 0;
    }
}
