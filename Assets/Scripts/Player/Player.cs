using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Last Modified: 8/23/21
/// 
/// Class: Player
///  
/// Author: Justin D'Errico
///
/// Description:
///    The player controller.
///    Makes use of a state machine
/// 
/// </summary>

public class Player : MonoBehaviour
{
    /* --- VARIABLES  --- */
    /* - Components - */
    public Rigidbody _rigidbody { get; private set; }
    public Collider _collider { get; private set; }
    public Renderer _renderer { get; private set; }
    public Player _player { get { return this; } }

    /* - Movement - */
    [Header("Movement Keys")]
    [SerializeField]
    private KeyCode up = KeyCode.Space;
    public KeyCode Up { get { return up; } private set { up = value; } }

    [Header("Movement Modifiers")]
    [SerializeField]
    [Range(1f, 10f)]
    private float moveSpeedMultiplier = 1f;
    public float MoveSpeedMultiplier { get { return moveSpeedMultiplier; } private set { moveSpeedMultiplier = value; } }

    /* - Movement Effects - */
    private float slowEffect = 1f;
    public float SlowEffect { get { return slowEffect; } set { slowEffect = value; } }

    /* - Movement Logic - */
    public bool isGrounded { get; set; }
    private float distToGround;
    private float dtgErrorMargin = .01f;

    /* - State - */
    public PlayerState currentState { get; private set; }


    /*-------------------------*/



    /* --- FUNCTIONS --- */
    /* - Grabbing references - */
    private void Start()
    {
        // Get components for referencing
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();

        distToGround = _collider.bounds.extents.y;

        SetState(new PlayerNormal(this));
    }

    /* - FixedUpdate for Rigidbody Physics / Movement - */
    private void FixedUpdate()
    {
        // movement script checks everytime if the player is grounded or not
        isGrounded = DetectGround();
        currentState.Tick();
    }



    /* - State Functions - */

    // Called to set the movement state of the player 
    public void SetState(PlayerState state)
    {
        if (currentState != null) { currentState.OnStateExit(); }

        currentState = state;

        if (currentState != null) { currentState.OnStateEnter(); }
    }



    /* - Movement Functions - */
    public void MovePlayer()
    {
        _rigidbody.velocity = new Vector2(0f, 1f * moveSpeedMultiplier);
    }

    private bool DetectGround()
    {
        // the extent of the collider
        float dist = _collider.bounds.extents.x - .01f;

        // checks at either end of the collider to determine if grounded
        bool right = Physics.Raycast(new Vector3(transform.position.x + dist, transform.position.y, transform.position.z), Vector3.down, distToGround + dtgErrorMargin);
        bool left = Physics.Raycast(new Vector3(transform.position.x - dist, transform.position.y, transform.position.z), Vector3.down, distToGround + dtgErrorMargin);

        // returns true if either side detects ground
        return left || right;
    }


    /*-------------------------*/
}
