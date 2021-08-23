using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Last Modified: 8/23/21
/// 
/// Class: PlayerNormal
///  
/// Author: Justin D'Errico
///
/// Description:
///    The state where player the players starts off in and enters when 
///    damaged or hits obstactle in boosted mode
/// 
/// </summary>

public class PlayerNormal : PlayerState
{
    // Constructor
    private Player playerRef;
    private Main gameRef;
    public PlayerNormal(Player playerRef, Main gameRef) : base(playerRef, gameRef)
    {
        this.playerRef = playerRef;
        this.gameRef = gameRef;
    }

    // Tick called every FixedUpdate in Player
    public override void Tick()
    {
        // First check if dead

        // if paused, pause. Else, do work
        if (gameRef.paused) { playerRef.SetState(new PlayerPaused(playerRef, gameRef, this)); }
        else
        {
            // Move player if the player is not grounded
            if (Input.GetKey(playerRef.Up) && playerRef.isGrounded) { playerRef.MovePlayer(); }
        }
    }

    // Called when entering state
    public override void OnStateEnter()
    {
        playerRef._renderer.material.color = Color.blue;
    }
}
