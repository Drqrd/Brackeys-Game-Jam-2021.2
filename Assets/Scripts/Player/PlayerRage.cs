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
    public PlayerRage(Player playerRef) : base(playerRef)
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
        playerRef._renderer.material.color = Color.red;
    }
}
