using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Last Modified: 8/22/21
/// 
/// Class: PlayerRunning
///  
/// Author: Justin D'Errico
///
/// Description:
///    Running state for player
///   
/// 
/// </summary>

public class PlayerRunning : PlayerState
{

    // Constructor calls
    private PlayerMovement p;
    public PlayerRunning(PlayerMovement p)
    {
        this.p = p;
        // For identifying the type of PlayerState
        type = "PlayerRunning";
    }

    public float slowEffect = 1f;

    public override void Tick() 
    {
        // Get the movement inputs
        Vector2 movement = p.MovementVector();

        // If movement requires you to switch states, then do so...
        if (movement == Vector2.zero) { p.SetState(new PlayerStanding(p)); }
        else if (p.MovementOnYAxis(movement)) { p.SetState(new PlayerJumping(p)); }

        // Otherwise move
        movement = ApplyEffects(movement);
        p._rigidbody.velocity = new Vector2(movement.x * p.RunMultiplier, p._rigidbody.velocity.y);
    }

    public override void OnStateEnter()
    {
        // Changes color for now, to identify the state the player is in
        p._renderer.material.color = Color.green;
    }

    // Applies all movement effects when running
    private Vector2 ApplyEffects(Vector2 movement)
    {
        // slows down the player when given a slowEffect value, does not slow jumping
        movement = new Vector3 (movement.x * slowEffect, movement.y);
        return movement;
    }
}
