using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Last Modified: 8/22/21
/// 
/// Class: SlowArea
///  
/// Author: Justin D'Errico
///
/// Description:
///    Obstacle that slows the player's movement when inside
/// 
/// </summary>

public class SlowArea : Area
{
    [Header("Parameters")]
    [Tooltip("Percent that the player is slowed by")]
    [SerializeField]
    [Range(0f, 1f)]
    private float slowPercent = .5f;

    [SerializeField]
    [Range(1f, 10f)]
    private float length = 1f;

    [SerializeField]
    [Range(1f, 10f)]
    private float width = 1f;

    // For easy adjustments in editor
    private void OnValidate()
    {
        GetComponent<BoxCollider>().size = new Vector2(length, width);
    }

    private void OnTriggerStay(Collider other)
    {
        // Applies the slow effect when inside the collider
        playerRef.GetComponent<Player>().SlowEffect = slowPercent;
    }

    private void OnTriggerExit(Collider other)
    {
        playerRef.GetComponent<Player>().SlowEffect = 1f;
    }

    // Draws a different colored box to differentiate between other areas in editor mode
    new private void OnDrawGizmos()
    {
        Gizmos.color = new Color(.65f, .16f, .16f, .5f);
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().bounds.size);
    }
}