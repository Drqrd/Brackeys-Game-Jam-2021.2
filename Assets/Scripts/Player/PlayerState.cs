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

public abstract class PlayerState
{
    public abstract void Tick();

    public virtual void HandleInputs() { }
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
}
