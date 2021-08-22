using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player
{
    /* --- VARIABLES --- */
    /* Movement Keys */

    // These key codes are the controls for the player, with self explanatory names
    [Header("Controls")]
    [SerializeField]
    private KeyCode left = KeyCode.A;
    [SerializeField]
    private KeyCode right = KeyCode.D,
                    jump = KeyCode.Space;

    /* - Movement Vals - */
    [Header("Movement Options")]
    [Tooltip("If true, player can move around while in the air.")]
    [SerializeField]
    private bool freeJump = false;

    [Tooltip("If true, player will come to immediate stop when reaching idle state.")]
    [SerializeField]
    private bool immediateStop = false;

    [SerializeField]
    [Range(0.1f, 10f)]
    private float jumpMultiplier = 1f;
        
    [SerializeField]
    [Range(0.1f, 10f)]
    private float runMultiplier = 1f;

    /* - Movement Logic (or for movement logic) - */
    public bool isGrounded { get; private set; }

    [Header("Ground Detection Error Margin")]
    [SerializeField]
    [Range(0.01f,1f)]
    private float dtgErrorMargin = .01f;
    private float distToGround;

    /* - State - */
    private PlayerState currentState;



    /*-------------------------*/



    /* --- GET / SET --- */

    public bool FreeJump
    {
        get { return freeJump; }
        private set { freeJump = value; }
    }

    public bool ImmediateStop
    {
        get { return immediateStop; }
        private set { immediateStop = value; }
    }


    public float JumpMultiplier
    {
        get { return jumpMultiplier; }
        private set { jumpMultiplier = value; }
    }

    public float RunMultiplier
    {
        get { return runMultiplier; }
        private set { runMultiplier = value; }
    }



    /*-------------------------*/



    /* --- FUNCTIONS --- */
    /* - Startup functions, FixedUpdate - */

    // On start, the player will be standing
    new private void Start()
    {
        // Get components
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();

        // Make sure the logic variables that need values, have values
        StartupVariables();

        // Set the starting state
        SetState(new PlayerStanding(this));
    }

    private void FixedUpdate()
    {
        // movement script checks everytime if the player is grounded or not
        isGrounded = DetectGround();
        currentState.Tick();
    }

    private void StartupVariables()
    {
        // distance to ground if the center of the collider is at (0,0,0)
        distToGround = _collider.bounds.extents.y;
    }



    /* - State Functions - */

    // Called to set the movement state of the player 
    public void SetState(PlayerState state)
    {
        if (currentState != null) { currentState.OnStateExit(); }

        currentState = state;

        if (currentState != null) { currentState.OnStateEnter(); }
    }

    public bool MovementOnXAxis(Vector3 v)
    {
        if (v.x > 0f || v.x < 0f) { return true; }
        return false;
    }

    public bool MovementOnYAxis(Vector3 v)
    {
        if (v.y > 0f || v.y < 0f) { return true; }
        return false;
    }

    private bool DetectGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + dtgErrorMargin); 
    }

    /* - Movement Functions - */

    // Totals the movement vector based on keys
    public Vector3 MovementVector()
    {
        Vector3 v = Vector3.zero;

        if (Input.GetKey(left)) { v += Vector3.left; }
        if (Input.GetKey(right)) { v += Vector3.right; }
        if (Input.GetKey(jump)) { v += Vector3.up; }

        return v;
    }

}
