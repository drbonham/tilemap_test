using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    Animator animator;
    Player player;
    PlayerMovement playerMovement;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
    }
	
	// Update is called once per frame
	void Update () {
        switch (player.state)
        {
            case CharacterState.Idle:
                animator.SetInteger("state", 0);
                animator.SetFloat("lastMoveX", playerMovement.lastMoveDir.x);
                animator.SetFloat("lastMoveY", playerMovement.lastMoveDir.y);
                break;
            case CharacterState.Moving:
                animator.SetInteger("state", 1);
                animator.SetFloat("moveX", playerMovement.moveDirection.x);
                animator.SetFloat("moveY", playerMovement.moveDirection.y);
                break;
            case CharacterState.Slashing:
                animator.SetInteger("state", 2);
                animator.SetFloat("lastMoveX", playerMovement.lastMoveDir.x);
                animator.SetFloat("lastMoveY", playerMovement.lastMoveDir.y);
                
                break;
        }
    }

    void slashAnimationEnded()
    {
        player.state = CharacterState.Idle;
    }
}
