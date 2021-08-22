using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    public abstract void Tick();

    public virtual void HandleInputs() { }
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
}
