using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Last Modified: 8/22/21
/// 
/// Class: PlayerStanding
///  
/// Author: Justin D'Errico
///
/// Description:
///    Standing state for player
/// 
/// </summary>

public class PlayerStanding : PlayerState
{
    
    // Constructor calls
    private PlayerMovement p;
    public PlayerStanding(PlayerMovement p)
    {
        this.p = p;
        // For identifying the type of PlayerState
        type = "PlayerStanding";
    }

    public override void Tick()
    {
        // Get the movement inputs
        Vector2 movement = p.MovementVector();

        // If movement requires you to switch states, then do so...
        if (p.MovementOnYAxis(movement)) { p.SetState(new PlayerJumping(p)); }
        else if (p.MovementOnXAxis(movement)) { p.SetState(new PlayerRunning(p)); }

        // Otherwise idle
        if (p.ImmediateStop) { p._rigidbody.velocity = new Vector2(0f, p._rigidbody.velocity.y); }
    }

    public override void OnStateEnter()
    {
        // Changes color for now, to identify the state the player is in
        p._renderer.material.color = Color.blue;

        // Enables ground friction on entering this state to slow the player down
        p.GroundFriction(true);
    }

    public override void OnStateExit()
    {
        // Removes ground friction after leaving state
        p.GroundFriction(false);
    }
}
