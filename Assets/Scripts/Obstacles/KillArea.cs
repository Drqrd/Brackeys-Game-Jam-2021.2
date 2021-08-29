using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillArea : Area
{
    // Sprites array for holding the car sprites
    [Header("Sprites")]
    [SerializeField]
    public Sprite[] sprites;

    [Header("Destruction Sprite Name")]
    [SerializeField]
    private string desName;

    // Points to add when destroyed
    private int points = 500;

    private Sprite[] desSprites;

    private bool breaking = false;

    new private void Start()
    {
        base.Start();

        desSprites = Resources.LoadAll<Sprite>("Sprites/Obstacles/Individual/" + desName);

        // 8 different particles
        for (int i = 0; i < 8; i++)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Particle System"), transform.Find("Particle System"));
            
            // Convert sprite to texture for material for particle system
            Sprite sprite = desSprites[i];
            Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                      (int)sprite.textureRect.y,
                                                      (int)sprite.textureRect.width,
                                                      (int)sprite.textureRect.height);
            texture.SetPixels(pixels);
            texture.Apply();

            Material material = new Material(Shader.Find("Standard"));
            material.mainTexture = texture;

            obj.GetComponent<ParticleSystemRenderer>().material = material;
        }
    }



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

    private void BreakObject()
    {
        if (!breaking)
        {
            breaking = true;
            // Disable sprite renderer
            base.audioRef.playAudio("explosion");
            GetComponent<SpriteRenderer>().enabled = false;

            Transform t = transform.Find("Particle System");
            foreach (Transform child in t)
            {
                child.GetComponent<ParticleSystem>().Play();
            }

            gameRef.GetComponent<Main>().AddPoints(points);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.name == "Player") 
        { 
            if (playerRef.GetComponent<Player>().currentState.type != "PlayerRage")
            {
                playerRef.GetComponent<Player>().isDamaged = true;
            }
            else
            {
                BreakObject();
            }
        }
            
    }

    new protected void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 1f);
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Collider"))
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
}
