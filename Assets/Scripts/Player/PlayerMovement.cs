using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Last Modified: 8/22/21
/// 
/// Class: PlayerMovement
///  
/// Author: Justin D'Errico
///
/// Description:
///    Script that inherits player, handles all movement
/// 
/// </summary>

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
    [Range(1f, 25f)]
    private float jumpMultiplier = 1f;
        
    [SerializeField]
    [Range(0.1f, 10f)]
    private float runMultiplier = 1f;

    [SerializeField]
    [Range(0.1f, 10f)]
    private float frictionMultiplier = 1f;

    /* - Movement Logic (or for movement logic) - */
    public bool isGrounded      { get; private set; }
    public bool leftCollision   { get; private set; }
    public bool rightCollision  { get; private set; }
    public bool topCollision    { get; private set; }
    public bool bottomCollision { get; private set; }

    [Header("Ground Detection Error Margin")]
    [SerializeField]
    [Range(0.01f,1f)]
    private float dtgErrorMargin = .01f;
    private float distToGround;

    private PhysicMaterial friction, noFriction;

    /* - State - */
    public PlayerState currentState { get; private set; }



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

    public float FrictionMultiplier
    {
        get { return FrictionMultiplier; }
        private set { frictionMultiplier = value; }
    }

    public PhysicMaterial Friction
    {
        get { return friction; }
        private set { friction = value; }
    }

    public PhysicMaterial NoFriction
    {
        get { return noFriction; }
        private set { noFriction = value; }
    }

    /*-------------------------*/



    /* --- FUNCTIONS --- */
    /* - Startup functions, FixedUpdate - */

    // On start, the player will be standing
    new private void Start()
    {
        base.Start();

        // Make sure the variables that need values, have values
        StartupVariables();

        // Set the starting state
        SetState(new PlayerStanding(this));

        Gravity();
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

        // Physic material resources for applying friction to player to slow them down when standing
        Friction   = Resources.Load<PhysicMaterial>("PhysicMaterials/Friction");
        NoFriction = Resources.Load<PhysicMaterial>("PhysicMaterials/NoFriction");

    }



    /* - State Functions - */

    // Called to set the movement state of the player 
    public void SetState(PlayerState state)
    {
        if (currentState != null) { currentState.OnStateExit(); }

        currentState = state;

        if (currentState != null) { currentState.OnStateEnter(); }
    }

    // Small bool function that returns true if there is x velocity
    public bool MovementOnXAxis(Vector2 v)
    {
        if (v.x > 0f || v.x < 0f) { return true; }
        return false;
    }

    // Small bool function that returns true if there is y velocity
    public bool MovementOnYAxis(Vector2 v)
    {
        if (v.y > 0f || v.y < 0f) { return true; }
        return false;
    }

    private bool DetectGround()
    {   
        // the extent of the collider
        float dist = _collider.bounds.extents.x - .01f;
        
        // checks at either end of the collider to determine if grounded
        bool right = Physics.Raycast(new Vector3(transform.position.x + dist, transform.position.y, transform.position.z), Vector3.down, distToGround + dtgErrorMargin);
        bool left  = Physics.Raycast(new Vector3(transform.position.x - dist, transform.position.y, transform.position.z), Vector3.down, distToGround + dtgErrorMargin);

        // returns true if either side detects ground
        return left || right;
    }

    /* - Movement Functions - */

    // Totals the movement vector based on keys
    public Vector2 MovementVector()
    {
        Vector2 v = Vector2.zero;

        if (Input.GetKey(left))  { v += Vector2.left;  }
        if (Input.GetKey(right)) { v += Vector2.right; }
        if (Input.GetKey(jump))  { v += Vector2.up;    }

        return v;
    }

    // If val = true, enable friction, disable if false
    public void GroundFriction(bool val)
    {
        if (val) { _collider.material = Friction; }
        else { _collider.material = NoFriction; }
    }



    /* - Collision - */
    /*
    private void OnCollisionEnter(Collision collision)
    {
        string sideHit = DetectCollisionSide(collision);
        CollisionCase(sideHit);

    }

    // From a collision, detects what side the player collided on through raycasting from the player
    // to the object in question, this is mainly to work around colliders getting caught on one another
    private string DetectCollisionSide(Collision collision)
    {
        Vector3 localPoint = collision.transform.InverseTransformPoint(collision.GetContact(0).point);
        Vector3 localDir   = localPoint.normalized;

        // If up is positive, above else below, etc.
        float upDot    = Vector3.Dot(localDir, Vector3.up);
        float rightDot = Vector3.Dot(localDir, Vector3.right);

        // The highest value shows if we are more right than we are up, etc.
        float upPower    = Mathf.Abs(upDot);
        float rightPower = Mathf.Abs(rightDot);


        if (upPower > rightPower)
        {
            if (upDot > 0f) { return "Up"; }
            else { return "Down"; }
        }
        else if (rightDot > 0f) { return "Right"; }
        else { return "Left"; }
    }

    private void CollisionCase(string dir)
    {
        switch (dir)
        {
            case "Up":
                topCollision = true;
                bottomCollision = false;
                leftCollision = false;
                rightCollision = false;
                break;
            case "Down":
                topCollision = false;
                bottomCollision = true;
                leftCollision = false;
                rightCollision = false;
                break;
            case "Left":
                topCollision = false;
                bottomCollision = false;
                leftCollision = true;
                rightCollision = false;
                break;
            case "Right":
                topCollision = false;
                bottomCollision = false;
                leftCollision = false;
                rightCollision = true;
                break;
            default:
                break;
        }
    }
    */
    /* - Temp Functions - */ 
    // Temporary location for gravity script
    private void Gravity()
    {
        Physics.gravity = new Vector3(0f, -30f, 0f);
    }
}
