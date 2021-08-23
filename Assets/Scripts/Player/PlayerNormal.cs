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
    public PlayerNormal(Player playerRef) : base(playerRef)
    {
        this.playerRef = playerRef;
    }

    // Tick called every FixedUpdate in Player
    public override void Tick()
    {
        if (Input.GetKeyDown(playerRef.Up) && playerRef.isGrounded) { playerRef.MovePlayer(); }
    }

    // Called when entering state
    public override void OnStateEnter()
    {
        playerRef._renderer.material.color = Color.blue;
    }
}
