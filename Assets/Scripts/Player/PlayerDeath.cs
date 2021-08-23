using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : PlayerState
{
    // Constructor
    private Player playerRef;
    private Main gameRef;
    public PlayerDeath(Player playerRef, Main gameRef) : base(playerRef, gameRef)
    {
        this.playerRef = playerRef;
        this.gameRef = gameRef;
    }

    // Tick called every FixedUpdate in Player
    public override void Tick()
    {
        if (Input.GetKeyDown(playerRef.Up) && playerRef.isGrounded) { playerRef.MovePlayer(); }
    }

    // Called when entering state
    public override void OnStateEnter()
    {
        playerRef._renderer.material.color = Color.black;
    }
}
