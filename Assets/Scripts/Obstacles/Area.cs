using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Last Modified: 8/22/21
/// 
/// Class: Area
///  
/// Author: Justin D'Errico
///
/// Description:
///    Area class inherited by all 
/// 
/// </summary>

[RequireComponent(typeof(BoxCollider))]
public class Area : MonoBehaviour
{
    // All inherited members will need to reference the player
    protected GameObject playerRef;

    // Get player reference
    private void Start()
    {
        playerRef = GameObject.Find("Player");
    }

    // A little gizmo to see the area in which the class operates
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 0f, 0f, 0.5f);
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().bounds.size);
    }
}
