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
    [Range(1f, 100f)]
    private float jumpPowerMultiplier = 25f;
    public float JumpPowerMultiplier { get { return jumpPowerMultiplier; } private set { jumpPowerMultiplier = value; } }

    [SerializeField]
    [Range(9.8f, 40f)]
    private float gravity = 9.8f;

    [SerializeField]
    [Range(4, 8)]
    private int maxJumpHeight = 4;


    /* - Movement Effects - */
    private float slowEffect = 1f;
    public float SlowEffect { get { return slowEffect; } set { slowEffect = value; } }

    /* - Movement Logic - */
    public bool isGrounded { get; set; }
    private float distToGround;
    private float dtgErrorMargin = .01f;
    private float groundY;


    /* - State - */
    public PlayerState currentState { get; private set; }

    /* - Game Controller Reference - */
    private Main gameRef;


    /*-------------------------*/



    /* --- FUNCTIONS --- */
    /* - Grabbing references - */
    private void Start()
    {
        // Get components for referencing
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<SpriteRenderer>();
        gameRef = GameObject.Find("GameController").GetComponent<Main>();

        // Various variables that need values at runtime
        distToGround = _collider.bounds.extents.y;
        groundY = transform.position.y;

        // Set gravity = to value in inspector
        Physics.gravity = new Vector3(0f, -gravity, 0f);

        // Start in the paused state for the main menu
        SetState(new PlayerPaused(this, gameRef));
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

    // Moves the player up when key is pressed
    public void MovePlayer()
    {
        _rigidbody.velocity = new Vector2(0f, 1f * jumpPowerMultiplier);
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

    public bool AtMaxJumpHeight()
    {
        return transform.position.y > groundY + maxJumpHeight;
    }



    /*-------------------------*/
}
