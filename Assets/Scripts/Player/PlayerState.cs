using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Last Modified: 8/22/21
/// 
/// Class: PlayerState
///  
/// Author: Justin D'Errico
///
/// Description:
///    Player state parent class, is inherited by all states
/// 
/// </summary>

public abstract class PlayerState
{
    // Constructor
    Player playerRef;
    Main gameRef;
    public PlayerState(Player playerRef, Main gameRef)
    {
        this.playerRef = playerRef;
        this.gameRef = gameRef;
    }


    // type declaration for determining what state
    public string type = "PlayerDefault";
    
    // Needed in each state function, is called per FixedUpdate
    public abstract void Tick();
    
    // Called when entering state
    public virtual void OnStateEnter() { }

    // Called when exiting state
    public virtual void OnStateExit() { }
}
