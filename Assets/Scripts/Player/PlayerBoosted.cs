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
    public PlayerBoosted(Player playerRef) : base(playerRef)
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
        playerRef._renderer.material.color = Color.green;
    }
}
