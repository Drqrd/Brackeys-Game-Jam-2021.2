using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Last Modified: 8/22/21
/// 
/// Class: Main
///  
/// Author: Justin D'Errico
///
/// Description:
///    Has various parameters relevant to the game such as clocks and statistics recording
/// 
/// </summary>

public class Main : MonoBehaviour
{

    /* --- VARIABLES -- */
    /* - Game Parameters - */
    [Header("Game Parameters")]
    [Tooltip("secondsPerMultiplier = 30, every 30 seconds the speed increases by 1 times. 30s = 2x, 60s = 3x, etc.")]
    [SerializeField]
    [Range(15f, 120f)]
    private float secondsPerMultiplier = 30f;
    public bool paused { get; set; }

    /* - Records - */
    [Tooltip("The interval at which the time is counted. Ex: 1f = 1s, .5f = 1/2s, etc.")]
    [SerializeField]
    [Range(.01f, 1f)]
    private float timeInterval;

    // For Stats section
    public float distanceTraveled { get; set; }
    public float timeTraveled { get; set; }

    // For determining time traveled
    private float startTime;
    public  float endTime { get; set; }
    private float timeElapsed;

    // For clock, used in game function
    private float clock = 0f;

    public float Clock { get { return clock; } }
    /* - Game Mechanics - */
    // For game speed, how fast objects translate across the screen
    private float gameSpeed;
    public float GameSpeed { get { return gameSpeed * 3f; } set { gameSpeed = value; } }
    public float leftBound { get; private set; }
    public float teleportPoint { get; private set; }

    [SerializeField]
    [Range(9.8f, 40f)]
    private float gravity = 9.8f;

    // Points
    public static int points { get; private set; }


    /*-------------------------*/



    /* --- FUNCTIONS --- */

    /* - Initialization - */
    private void Start()
    {
        // Game state at start
        paused = true;

        // Recording time
        timeTraveled = 0f;
        startTime    = Time.time;
        timeElapsed  = startTime;

        // Set gravity = to value in inspector
        Physics.gravity = new Vector3(0f, -gravity, 0f);

        // values for teleporting obstacles
        //              the center position                1/2 the extent of what the camera sees in world position      padding
        leftBound     = Camera.main.transform.position.x - (Camera.main.orthographicSize * Screen.width / Screen.height) - 2f;
        teleportPoint = Camera.main.transform.position.x + (Camera.main.orthographicSize * Screen.width / Screen.height) + 2f;


        points = 0;
    }

    private void FixedUpdate()
    {
        gameSpeed = GameSpeedFunction(clock);
    }

    /* - Update for clock - */
    private void Update()
    {
        if (!paused)
        {
            Time.timeScale = 1f;
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= timeInterval)
            {
                timeElapsed -= timeInterval;
                clock += timeInterval;
                //Debug.Log(clock);
            }
        }
        else { Time.timeScale = 0f; }
        
    }

    /* - Game Speed Function - */
    private float GameSpeedFunction(float x)
    {
        return x / secondsPerMultiplier + 1f;
    }



    /* - Score Functions - */
    public void AddPoints(int p)
    {
        points += p;
        //Debug.Log(points);
    }

    /*-------------------------*/
}
