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
    // Constructor
    private Player playerRef;
    private Main gameRef;
    public PlayerBoosted(Player playerRef, Main gameRef) : base(playerRef, gameRef)
    {
        this.playerRef = playerRef;
        this.gameRef = gameRef;
    }

    // Tick called every FixedUpdate in Player
    public override void Tick()
    {
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
        playerRef._renderer.material.color = Color.green;
    }
}
