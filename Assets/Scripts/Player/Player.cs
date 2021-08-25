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
    public SpriteRenderer _renderer { get; private set; }

    public Player _player { get { return this; } }

    private Vector3 initialPosition;

    [Header("Player Settings")]
    [SerializeField]
    public Sprite[] playerSprites;

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
    [Range(4, 8)]
    private int maxJumpHeight = 4;

    public float movementSpeed = 4f;



    /* - Movement Effects - */
    private float slowEffect = 1f;
    public float SlowEffect { get { return slowEffect; } set { slowEffect = value; } }

    /* - Movement Logic - */
    public bool isGrounded { get; set; }
    private float distToGround;
    private float dtgErrorMargin = .01f;
    private float groundY;
    


    /* - State + Logic - */
    public PlayerState currentState { get; private set; }
    public bool isDamaged { get; set; }

    /* - Game Controller Reference - */
    private Main gameRef;


    /*-------------------------*/



    /* --- FUNCTIONS --- */
    /* - Grabbing references - */
    private void Start()
    {
        // Get components for referencing
        _rigidbody = GetComponent<Rigidbody>();
        // Get foot collider
        _collider = transform.GetChild(2).GetComponent<Collider>();
        _renderer = GetComponent<SpriteRenderer>();
        gameRef = GameObject.Find("GameController").GetComponent<Main>();

        // Various variables that need values at runtime
        distToGround = _collider.bounds.extents.y;
        groundY = transform.position.y;
        initialPosition = transform.position;

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
    public void movePlayer()
    {
        _rigidbody.velocity = new Vector2( movementSpeed, _rigidbody.velocity.y);
        gameRef.distanceTraveled =  Mathf.Sqrt(Mathf.Pow(transform.position.x - initialPosition.x, 2));
    }

    public void playerJump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 1f * jumpPowerMultiplier);
    }

    private bool DetectGround()
    {
        // the extent of the collider
        float dist = _collider.bounds.extents.x - .01f;

        // checks at either end of the collider to determine if grounded
        bool right = Physics.Raycast(new Vector3(transform.position.x + dist, _collider.bounds.center.y, transform.position.z), Vector3.down, distToGround + dtgErrorMargin);
        bool left = Physics.Raycast(new Vector3(transform.position.x - dist, _collider.bounds.center.y, transform.position.z), Vector3.down, distToGround + dtgErrorMargin);

        // returns true if either side detects ground
        return left || right;
    }

    public bool AtMaxJumpHeight()
    {
        return transform.position.y > groundY + maxJumpHeight;
    }



    /*-------------------------*/
}
