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
    public float endTime { get; set; }
    private float timeElapsed;

    // For clock, used in game function
    private float clock = 0f;

    // For game speed, how fast objects translate across the screen
    public float gameSpeed { get; set; }

    /*-------------------------*/



    /* --- FUNCTIONS --- */

    /* - Initialization - */
    private void Start()
    {
        paused = true;
        timeTraveled = 0f;
        startTime = Time.time;
        timeElapsed = startTime;
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
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= timeInterval)
            {
                timeElapsed -= timeInterval;
                clock += timeInterval;
                Debug.Log(clock);
            }
        }
    }

    /* - Game Speed Function - */
    private float GameSpeedFunction(float x)
    {
        return x / secondsPerMultiplier + 1f;
    }


    /*-------------------------*/
}
