using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillArea : Area
{
    // Sprites array for holding the car sprites
    [Header("Sprites")]
    [SerializeField]
    private Sprite[] sprites;

    private void FixedUpdate()
    {
        Move();
    }


    // Whenever the car is teleported to the front, this script is accessed
    public override void Teleport()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        transform.position = new Vector3(gameRef.GetComponent<Main>().teleportPoint, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.name == "Player") { playerRef.GetComponent<Player>().isDamaged = true; } 
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
