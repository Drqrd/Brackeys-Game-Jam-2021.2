using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
///  Class: Player
///  
///  Description:
///     The player controller.
///     Makes use of a state machine for movement
/// 
/// </summary>
 
public class PlayerJumping : PlayerState
{
    private PlayerMovement p;

    public PlayerJumping(PlayerMovement p)
    {
        this.p = p;
    }



    public override void Tick()
    {
        // Get the movement inputs
        Vector3 movement = p.MovementVector();

        // Jump
        if (p.isGrounded)
        {
            p._rigidbody.velocity = new Vector3(p._rigidbody.velocity.x, movement.y * p.JumpMultiplier, p._rigidbody.velocity.z);
        }

        // Allows the player to move in the air when they jump
        if (p.FreeJump)
        {
            p._rigidbody.velocity = new Vector3(movement.x * p.RunMultiplier, p._rigidbody.velocity.y, movement.z * p.RunMultiplier);
        }

        // Switch to different state after jumping is done
        if (p.isGrounded)
        {
            if (movement == Vector3.zero) { p.SetState(new PlayerStanding(p)); }
            else { p.SetState(new PlayerRunning(p)); }
        }
    }

    public override void OnStateEnter()
    {
        p._renderer.material.color = Color.red;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}
