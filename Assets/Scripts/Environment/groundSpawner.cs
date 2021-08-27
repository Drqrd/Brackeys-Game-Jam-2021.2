using UnityEngine;

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


    private Sprite currentGround;
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
     
    


    private void Start()
    {
        gameRef = GameObject.Find("GameController").GetComponent<Main>();
        obstSpawner = GetComponent<obstaclesSpawn>();
        playerPos = GameObject.Find("Player").transform.position;
        lowestSpawnLevel = transform.GetChild(0).transform.position.y;
    }

    
    private void Update()
    {
        if(!gameRef.paused)
            this.setDistance_travelledFrom_lastPoint();
    }


    private void setDistance_travelledFrom_lastPoint(){   
        if( gameRef.distanceTraveled - lastDist_point >= spawnDistance_break ){
            this.spawnGround();
            this.destroyUsed_ground();
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
        for(int spawnedCount = 0; spawnedCount < Random.Range(spawnDistance_break, spawnDistance_break * 5); spawnedCount++ ){ 
            GameObject groundSpawned = Instantiate(groundPrefab);
            groundSpawned.GetComponent<SpriteRenderer>().sprite = currentGround;
            groundSpawned.transform.position = newGroundPos;
            groundSpawned.transform.parent = this.gameObject.transform;
            this.spawnBedrockLayer(newGroundPos);
            newGroundPos.x ++;
        }
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
            currentGround = blueGround[ Random.Range(0, blueGround.Length - 1) ];
        else if(drawNum >= 10 && drawNum < 25)
            currentGround = brownGround[ Random.Range(0, brownGround.Length - 1) ];
        else
            currentGround = greenGround[Random.Range(0, greenGround.Length - 1) ];
    }




    //DESTROY
    private void destroyUsed_ground(){
        for(int childCount = 0; childCount < transform.childCount; childCount++){
            GameObject groundChild = transform.GetChild(childCount).gameObject;
            ground groundChildScript = groundChild.GetComponent<ground>();
            groundChildScript.mainCam = Camera.main;
            groundChildScript.groundFocus = groundChildScript.setGround_focus();
            if(groundChildScript.groundFocus == 0){
                Destroy(groundChild);
            }
                
        }
    }


    
}
