using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Last Modified: 8/23/21
/// 
/// Class: PlayerBoosted
///  
/// Author: Justin D'Errico
///
/// Description:
///    After a set amount of time, the player enters this state and accelerates at a faster pace
/// 
/// </summary>

public class PlayerBoosted : PlayerState
{
    private float normalSpeed;
    private float frequency = 5f;

    // Constructor
    private Player playerRef;
    private Main gameRef;

    public PlayerBoosted(Player playerRef, Main gameRef) : base(playerRef, gameRef)
    {
        this.playerRef = playerRef;
        this.gameRef = gameRef;

        type = "PlayerBoosted";

        normalSpeed = playerRef.movementSpeed;
    }

    // Tick called every FixedUpdate in Player
    public override void Tick()
    {
        // Change movement speed every now and then
        VariateMovementSpeed();

        // Move the player
        playerRef.movePlayer();

        // if damaged, go to playerNormal
        if (playerRef.isDamaged) { playerRef.SetState(new PlayerNormal(playerRef, gameRef)); }

        // If you are in a chain, start rage mode
        if (playerRef.killCount >= playerRef.chain)
        {
            playerRef.killCount -= playerRef.chain;
            playerRef.SetState(new PlayerRage(playerRef, gameRef));
        }

        // if paused, pause. Else, do work
        if (gameRef.paused) { playerRef.SetState(new PlayerPaused(playerRef, gameRef, this)); }
        else
        {
            playerRef.movePlayer();
            if (playerRef.AtMaxJumpHeight())
            {
                playerRef._rigidbody.velocity = Vector3.zero;
            }
            // Move player if the player is not grounded
            else if (Input.GetKey(playerRef.Up) && playerRef.isGrounded)
            {
                playerRef.playerJump();
            }
        }
    }


    // Called when entering state
    public override void OnStateEnter()
    {
        playerRef._renderer.sprite = playerRef.playerSprites[1];
        playerRef.movementSpeed = 6f;
    }

    // Changes the movement speed depending on frequency of change and normal speed
    private void VariateMovementSpeed()
    {
        // frequency in changes
        if (Mathf.Approximately(gameRef.Clock % frequency, 0f)) { playerRef.movementSpeed = Random.Range(normalSpeed, normalSpeed * 1.25f); }
    }
}
