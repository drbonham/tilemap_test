using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 120f;
    float xMin, xMax, yMin, yMax;
    Rigidbody2D rigidBody;
    float moveHorizontal, moveVertical;
    public Vector3 moveDirection;
    public Vector2 lastMoveDir;
    Player player;

    // Use this for initialization
    void Start () {

        if (TileMapChunkGeneratorV2._Instance != null)
        {
            Vector3[] bounds = TileMapChunkGeneratorV2._Instance.MapBounds();
            xMin = bounds[0].x;
            xMax = bounds[1].x;
            yMin = bounds[0].y;
            yMax = bounds[1].y;
        }
        else
        {
            xMin = yMin = 0;
            xMax = yMax = 3000;
        }
        rigidBody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();

    }

    // Update is called once per frame
    public void HandleInput() {
        if (player.state != CharacterState.Idle && player.state != CharacterState.Moving)
        {
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveHorizontal, moveVertical, 0);
        if (moveHorizontal == 0 && moveVertical == 0)
        {
            player.state = CharacterState.Idle;
        }
        else
        {
            player.state = CharacterState.Moving;
            lastMoveDir = moveDirection;
        }
    }

    /// <summary>
    /// Call this in FixedUpdate for physics based movement updates
    /// </summary>
    public void FixedMovementUpdate()
    {
        if (player.state != CharacterState.Idle && player.state != CharacterState.Moving)
        {
            StopMovement();
            return;
        }
        //rigidBody.velocity = Vector3.zero;
        rigidBody.velocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
    }

    public void StopMovement()
    {
        rigidBody.velocity = Vector3.zero;
    }
}
