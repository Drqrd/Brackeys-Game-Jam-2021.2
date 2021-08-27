using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Last Modified: 8/23/21
/// 
/// Class: PlayerRage
///  
/// Author: Justin D'Errico
///
/// Description:
///    Player rage state, in which the player is able to destroy obstacles
/// 
/// </summary>

public class PlayerRage : PlayerState
{
    // Constructor
    private Player playerRef;
    private Main gameRef;
    public PlayerRage(Player playerRef, Main gameRef) : base(playerRef, gameRef)
    {
        this.playerRef = playerRef;
        this.gameRef = gameRef;

        type = "PlayerRage";
    }

    // Tick called every FixedUpdate in Player
    public override void Tick()
    {
        playerRef.movePlayer();
        // if paused, pause. Else, do work
        if (gameRef.paused) { playerRef.SetState(new PlayerPaused(playerRef, gameRef, this)); }
        else
        {
            // Move player if the player is not grounded
            if (Input.GetKey(playerRef.Up) && playerRef.isGrounded) { playerRef.playerJump(); }
        }
    }

    // Called when entering state
    public override void OnStateEnter()
    {
        playerRef._renderer.sprite = playerRef.playerSprites[2];
        playerRef.movementSpeed = 5f;
    }

    public override void OnStateExit()
    {
        // Make sure any damage incurred doesnt stay after exitting
        playerRef.isDamaged = false;
    }
}
