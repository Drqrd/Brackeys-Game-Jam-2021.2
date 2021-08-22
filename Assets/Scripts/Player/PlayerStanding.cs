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

public class PlayerStanding : PlayerState
{
    private PlayerMovement p;
    public PlayerStanding(PlayerMovement p)
    {
        this.p = p;
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
        p._renderer.material.color = Color.blue;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
