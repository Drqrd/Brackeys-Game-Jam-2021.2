using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaused : PlayerState
{
    // Constructor
    private Player playerRef;
    private Main gameRef;
    private PlayerState previousState;
    public PlayerPaused(Player playerRef, Main gameRef, PlayerState previousState = null) : base(playerRef, gameRef)
    {
        this.playerRef = playerRef;
        this.gameRef = gameRef;
        this.previousState = previousState;

        type = "PlayerPaused";
    }

    // Tick called every FixedUpdate in Player
    public override void Tick()
    {
        // When the game unpauses, start the game
        if (!gameRef.paused)
        {
            // If there is no previous state, set to normal, else use previous state
            if (previousState == null) { playerRef.SetState(new PlayerNormal(playerRef, gameRef)); }
            else { playerRef.SetState(previousState); }
        }
    }

    // Called when entering state
    public override void OnStateEnter()
    {
        playerRef._renderer.sprite = playerRef.playerSprites[3];
        
    }
}
