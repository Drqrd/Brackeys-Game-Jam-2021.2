using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Last Modified: 8/22/21
/// 
/// Class: PlayerJumping
///  
/// Author: Justin D'Errico
///
/// Description:
///    The state the player goes into when jumping
/// 
/// </summary>

public class PlayerJumping : PlayerState
{

    // Constructor calls
    private PlayerMovement p;
    public PlayerJumping(PlayerMovement p)
    {
        this.p = p;
        // For identifying the type of PlayerState
        type = "PlayerJumping";
    }



    public override void Tick()
    {
        // Get the movement inputs
        Vector2 movement = p.MovementVector();

        // Jump if player is grounded
        if (p.isGrounded)
        {
            p._rigidbody.velocity = new Vector2(p._rigidbody.velocity.x, movement.y * p.JumpMultiplier);
        }

        // Allows the player to move in the air when they jump
        if (p.FreeJump)
        {
            // movement = LimitMovement(movement);
            p._rigidbody.velocity = new Vector2(movement.x * p.RunMultiplier, p._rigidbody.velocity.y);
        }

        // Switch to different state after jumping is done, when player is grounded
        if (p.isGrounded)
        {
            if (movement == Vector2.zero) { p.SetState(new PlayerStanding(p)); }
            else { p.SetState(new PlayerRunning(p)); }
        }
    }

    public override void OnStateEnter()
    {
        // Changes color for now, to identify the state the player is in
        p._renderer.material.color = Color.red;
    }

    /*
    private Vector2 LimitMovement(Vector2 movement)
    {
        if ((p.leftCollision && movement.x > 0f) || (p.rightCollision && movement.x < 0f)) { movement = new Vector2(0f, movement.y); }
        return movement;
    }
    */
}
