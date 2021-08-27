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
///    Human script, makes human die on collision with player
/// 
/// </summary>

public class HumanArea : Area
{   
    // Sprites array for holding the car sprites
    [Header("Sprites")]
    [SerializeField]
    private Sprite[] sprites;

    private int points = 200;
    private bool notCalled = true;

    private void FixedUpdate()
    {
        Move();
    }
    public override void Teleport()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.name == "Player")
        {
            if (notCalled)
            {
                AddPoints();
                KillHuman();
                notCalled = false;
            }
        }
    }

    private void KillHuman()
    {
        // Play blood animation
        GetComponent<ParticleSystem>().Play();
        GetComponent<SpriteRenderer>().enabled = false; 
    }

    private void AddPoints()
    {
        // Add 1 to the kill count
        playerRef.GetComponent<Player>().killCount += 1;

        // Add points to the score
        gameRef.GetComponent<Main>().AddPoints(points);
    }

    new protected void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 1f);
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Collider>() as BoxCollider)
            {
                Gizmos.DrawWireCube(child.GetComponent<BoxCollider>().bounds.center, child.GetComponent<BoxCollider>().bounds.size);
            }
            else if (child.GetComponent<Collider>() as SphereCollider)
            {
                Gizmos.DrawWireSphere(child.GetComponent<SphereCollider>().bounds.center, child.GetComponent<SphereCollider>().radius);
            }

        }
    }
}
