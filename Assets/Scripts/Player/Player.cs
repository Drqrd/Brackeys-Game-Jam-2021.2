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

public class Player : MonoBehaviour
{
    /* --- VARIABLES  --- */
    /* - Components - */
    public Rigidbody _rigidbody { get; protected set; }
    public Collider _collider { get; protected set; }
    public Renderer _renderer { get; protected set; }
    public Player _player { get { return this; } }



    /*-------------------------*/



    /* --- FUNCTIONS --- */
    /* - Grabbing references - */
    protected void Start()
    {
        // Get components for referencing
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
    }



    /*-------------------------*/
}
