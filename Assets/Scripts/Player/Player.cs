using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Tool selectedTool;
	// Use this for initialization
	void Start () {
        selectedTool = GetComponentInChildren<Tool>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && selectedTool != null)
        {
            selectedTool.TryGather();
        }
	}
}
