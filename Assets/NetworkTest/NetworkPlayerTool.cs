using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class NetworkPlayerTool : NetworkBehaviour {

    Tool selectedTool;
    // Use this for initialization
    void Start()
    {
        selectedTool = GetComponentInChildren<Tool>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButtonDown(0) && selectedTool != null)
        {
            selectedTool.TryGather();
        }
    }
}
