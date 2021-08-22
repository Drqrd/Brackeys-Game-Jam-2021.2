using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Last Modified: 8/22/21
/// 
/// Class: Player
///  
/// Author: Justin D'Errico
///
/// Description:
///    The player controller.
///    Makes use of a state machine for movement
/// 
/// </summary>

public class PlayerRunning : PlayerState
{
    private PlayerMovement p;
    public PlayerRunning(PlayerMovement p)
    {
        this.p = p;
    }

    public override void Tick() 
    {
        // Get the movement inputs
        Vector2 movement = p.MovementVector();

        // If movement requires you to switch states, then do so...
        if (movement == Vector2.zero) { p.SetState(new PlayerStanding(p)); }
        else if (p.MovementOnYAxis(movement)) { p.SetState(new PlayerJumping(p)); }

        // Otherwise move
        else { p._rigidbody.velocity = new Vector2(movement.x * p.RunMultiplier, p._rigidbody.velocity.y); }
    }

    public override void OnStateEnter()
    {
        p._renderer.material.color = Color.green;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
