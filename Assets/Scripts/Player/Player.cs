using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    Idle,
    Moving,
    Slashing
}

public class Player : MonoBehaviour {

    public CharacterState state;
    Tool selectedTool;
    PlayerMovement playerMovement;

    // Use this for initialization
    void Start () {
        selectedTool = GetComponentInChildren<Tool>();
        playerMovement = GetComponent<PlayerMovement>();
        state = CharacterState.Idle;
    }
	
	// Update is called once per frame
	void Update () {
        // Break this out into an attack script later
        if (Input.GetMouseButtonDown(0) && selectedTool != null && state != CharacterState.Slashing)
        {
            state = CharacterState.Slashing;
            playerMovement.StopMovement();
            selectedTool.TryGather();
        }
        playerMovement.HandleInput();

    }

    private void FixedUpdate()
    {
        playerMovement.FixedMovementUpdate();
    }
}
