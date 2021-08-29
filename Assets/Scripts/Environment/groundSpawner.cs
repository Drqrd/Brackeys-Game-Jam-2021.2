using UnityEngine;

// For list
using System.Collections.Generic;

// For enumerators
using System.Collections;

/// <summary>
/*
/// Last Modified: 8/24/21
/// 
/// Class: GroundSpawner
///  
/// Author: Shubham Tiwary
///
/// Description:
///    Handles spawing of floor tiles via this algorithm:
            1. Measures distance travelled by player, if distance is more than spawnDistance_break variable, starts spawing from the furthest floor tile
            2. Checks if ground has passed camera, if it has it will be deleted; 
/// 
*/
/// </summary>

public class groundSpawner : MonoBehaviour
{ 
    
    [SerializeField]  private GameObject groundPrefab;
    [SerializeField]  private Sprite bedrockSprite;
    [SerializeField] private Sprite[] greenGround;
    [SerializeField] private Sprite[] brownGround;
    [SerializeField] private Sprite[] blueGround;


    private Sprite[] currentGrounds;
    private obstaclesSpawn obstSpawner;
    private Main gameRef;
    private Vector3 playerPos;



    //after how many distance player travells should the ground spawn
    private float spawnDistance_break = 5f;
    private float lastDist_point = 0f;

    

    //furthest point of ground spawned(juding by x axis), ground start spawing from that point;
    public Vector3 furthestGroundPos;
    private float lowestSpawnLevel;
    private float highestSpawnLevel = 1f;

    /*-------------------------------------*/
    /* Justin D'Errico edits for bugfixing */
    private List<BoxCollider> bcs;
    /*-------------------------------------*/


    private void Start()
    {
        gameRef = GameObject.Find("GameController").GetComponent<Main>();
        obstSpawner = GetComponent<obstaclesSpawn>();
        playerPos = GameObject.Find("Player").transform.position;
        lowestSpawnLevel = transform.GetChild(0).transform.position.y;
        /*-------------------------------------*/
        /* Justin D'Errico edits for bugfixing */
        bcs = new List<BoxCollider>();
        bcs.Add(transform.Find("Collider").GetComponent<BoxCollider>());
        /*-------------------------------------*/
}


private void Update()
    {
        if(!gameRef.paused)
            this.setDistance_travelledFrom_lastPoint();
    }


    private void setDistance_travelledFrom_lastPoint(){   
        if( gameRef.distanceTraveled - lastDist_point >= spawnDistance_break ){
            this.spawnGround();
            StartCoroutine(this.destroyUsed_ground());
            lastDist_point = gameRef.distanceTraveled;
        }       
    }


    //SPAWN
    private void spawnGround(){
        this.checkFurthestPoint();
        float spawnLevel =  this.setSpawnLevel();     
        Vector3 newGroundPos = new Vector3(furthestGroundPos.x + 1, spawnLevel, furthestGroundPos.z);
        obstSpawner.spawnObstacle(newGroundPos);
        this.setGroundSprite();

        /*-------------------------------------*/
        /* Justin D'Errico edits for bugfixing */
        // Accessible variable for collider stuff
        int randRange = (int)Random.Range(spawnDistance_break, spawnDistance_break * 5);
        /*-------------------------------------*/
        
        // ^- Put here with an int cast -----------.
        for (int spawnedCount = 0; spawnedCount < randRange; spawnedCount++ ){ 
            GameObject groundSpawned = Instantiate(groundPrefab);
            groundSpawned.GetComponent<SpriteRenderer>().sprite = currentGrounds[Random.Range(0, currentGrounds.Length - 1)];
            groundSpawned.transform.position = newGroundPos;
            groundSpawned.transform.parent = this.gameObject.transform;
            this.spawnBedrockLayer(newGroundPos);
            newGroundPos.x ++;
        }
        /*-------------------------------------*/
        /* Justin D'Errico edits for bugfixing */
        SpawnCollider(randRange, newGroundPos);
        /*-------------------------------------*/
    }


    private void spawnBedrockLayer(Vector3 newGroundPos){
        float bedrockGroundLevel = newGroundPos.y - 1;
        while(bedrockGroundLevel > lowestSpawnLevel - 1){
            GameObject bedrockSpawned = Instantiate(groundPrefab);
            bedrockSpawned.GetComponent<SpriteRenderer>().sprite = bedrockSprite;
            bedrockSpawned.transform.position = new Vector3(newGroundPos.x, bedrockGroundLevel);
            bedrockSpawned.transform.parent = this.gameObject.transform;
            bedrockGroundLevel--;
        }
    }

    private void checkFurthestPoint(){
        for(int childCount = 0; childCount < transform.childCount; childCount++){
            if( transform.GetChild(childCount).position.x > furthestGroundPos.x ){
                furthestGroundPos = transform.GetChild(childCount).position;
            }
        }
    }

    private float setSpawnLevel(){
        float spawnLVL; 
        spawnLVL = Random.Range(lowestSpawnLevel, furthestGroundPos.y + 2.5f);  
        if(spawnLVL > highestSpawnLevel)
            spawnLVL = Random.Range(lowestSpawnLevel, highestSpawnLevel - 1);  
        return spawnLVL;
    }


    private void setGroundSprite(){
        int drawNum = Random.Range(0, 100);
        if(drawNum < 10)
            currentGrounds = blueGround;
        else if(drawNum >= 10 && drawNum < 25)
            currentGrounds = brownGround;
        else
            currentGrounds = greenGround;
    }




    //DESTROY
    private IEnumerator destroyUsed_ground(){
        for(int childCount = 0; childCount < transform.childCount; childCount++){
            
            // Execute if not the collider 
            if (!transform.GetChild(childCount).name.Contains("Collider"))
            {
                GameObject groundChild = transform.GetChild(childCount).gameObject;
                ground groundChildScript = groundChild.GetComponent<ground>();
                groundChildScript.mainCam = Camera.main;
                groundChildScript.groundFocus = groundChildScript.setGround_focus();
                if (groundChildScript.groundFocus == 0)
                {
                    Destroy(groundChild);
                }
            }


            /*-------------------------------------*/
            /* Justin D'Errico edits for bugfixing */
            DestroyCollider();
            /*-------------------------------------*/
            yield return 0;
        }
    }

    /*-------------------------------------*/
    /* Justin D'Errico edits for bugfixing */
    // Spawn collider to bugfix the friction bug
    private void SpawnCollider(int randRange, Vector3 newGroundPos)
    {
        GameObject collider = new GameObject("Collider");
        collider.transform.parent = transform;
        BoxCollider bc = collider.AddComponent<BoxCollider>();
        bcs.Add(bc);
        bc.size = new Vector2(randRange, newGroundPos.y - lowestSpawnLevel);
        bc.center = new Vector2(newGroundPos.x - randRange / 2f - 0.5f, (newGroundPos.y + lowestSpawnLevel) /2f + 0.5f);
        bc.material = Resources.Load<PhysicMaterial>("PhysicMaterials/NoFriction");
    }

    // Destroy collider if its center is past the left of the camera
    private void DestroyCollider()
    {
        float halfCamera = Camera.main.orthographicSize * Screen.width / Screen.height;
        float deleteZone = Camera.main.transform.position.x - halfCamera;

        int count = 0;
        // For each boxCollider in the list
        foreach(BoxCollider bc in bcs)
        {
            // If the right most of the box collider is past the delete zone
            if (bc.center.x + bc.bounds.size.x < deleteZone)
            {
                count++;
            }
        }

        Debug.Log("BC SIZE: " + bcs.Count);
        Debug.Log("INDEX SIZE: " + count);


        // Destroy after iterating
        for(int i = 0; i < count; i++)
        {
            // Destroy the gameObject the boxCollider is attached to
            Destroy(bcs[0].gameObject);

            // Remove the box collider from the list
            bcs.RemoveAt(0);
        }
        Debug.Log("BC SIZE: " + bcs.Count); 
    }

    /*-------------------------------------*/
}
