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

public abstract class Area : MonoBehaviour
{
    // All inherited members will need to reference the player
    protected GameObject playerRef;
    protected GameObject gameRef;
    protected Rigidbody _rb;
    protected bool isEnabled;

    protected audioManager audioRef;

    // Get player reference
    protected void Start()
    {
        playerRef = GameObject.Find("Player");
        gameRef = GameObject.Find("GameController");
        audioRef = GameObject.Find("AudioManager").GetComponent<audioManager>();
        _rb = GetComponent<Rigidbody>();

        foreach (Transform t in transform)
        {
            if (t.name.Contains("Collider"))
            {
                t.GetComponent<Collider>().isTrigger = true;
            }
        }
    }

    public abstract void Teleport();

    protected virtual void Move()
    {
        // _rb.velocity = new Vector3(-gameRef.GetComponent<Main>().GameSpeed,_rb.velocity.y,0f);
    }

    // A little gizmo to see the area in which the class operates
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 0f, 0f, 0.5f);
        Gizmos.DrawCube(GetComponent<BoxCollider>().bounds.center, GetComponent<BoxCollider>().bounds.size);
    }
}
