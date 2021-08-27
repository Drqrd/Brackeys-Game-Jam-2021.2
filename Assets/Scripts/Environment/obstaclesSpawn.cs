using UnityEngine;


/// <summary>
/*
/// Last Modified: 8/24/21
/// 
/// Class: ObstaclesSpawner
///  
/// Author: Shubham Tiwary
///
/// Description:
///    Handles spawing of Obstacles tiles via this algorithm:
            1. Measures if the obstacle can fit in target ground tile, if it can,
            2. Checks if there are too many obstales nearby, if not then spawns
/// 
*/ 
/// </summary>



public class obstaclesSpawn : MonoBehaviour
{
    
    
    public GameObject obstacleHolder;
    public GameObject humansHolder;
    public GameObject fillersHolder;


    [SerializeField] private Sprite[] fillerSprites;
    [SerializeField] private GameObject fillerPrefab;
    private Object[] longObstacles; 
    private Object[] tallObstacles;
    private Object[] humanObstacles;






    private void Start(){
        longObstacles = Resources.LoadAll("Prefabs/Long Obstacles");
        tallObstacles = Resources.LoadAll("Prefabs/Tall Obstacles");
        humanObstacles = Resources.LoadAll("Prefabs/Humans");
    }


    public void spawnObstacle(Vector3 spawnPos){
        Vector3 obstPos = new Vector3(spawnPos.x + 1, spawnPos.y + 1, spawnPos.z);
        if(drawChances(50, 100)){
            Object[] obstacleArray = drawChances(50, 100) ? longObstacles : tallObstacles;
            for(int spawnCount = 0; spawnCount < Random.Range(1, 5); spawnCount++){
                Object obstaclePrefab = obstacleArray[Random.Range(0, obstacleArray.Length)]; 
                GameObject obstacle = (GameObject) Instantiate(obstaclePrefab);
                obstPos = new Vector3( Random.Range(obstPos.x, obstPos.x + 2), Random.Range(obstPos.y, obstPos.y + 2), obstPos.z);
                this.spawnHuman(obstPos);
                this.spawnFillers(obstPos);
                obstacle.GetComponent<SpriteRenderer>().sprite = this.changeObstacleSprite(obstacle);
                obstacle.transform.position = obstPos;
                obstacle.transform.parent = obstacleHolder.transform;
                obstPos.x++; 
            }
        } 
    }


    private Sprite changeObstacleSprite(GameObject obstacle){
        KillArea obstKillArea = obstacle.GetComponent<KillArea>();
        return obstKillArea.sprites[Random.Range(0, obstKillArea.sprites.Length)];
    }


    private void spawnHuman(Vector3 obstPos){
        if(drawChances(25, 100)){
            Object humanPrefab = humanObstacles[Random.Range(0, humanObstacles.Length)]; 
            GameObject human = (GameObject) Instantiate(humanPrefab);
            Vector3 humanPos = new Vector3( Random.Range(obstPos.x - 1, obstPos.x + 2), Random.Range(obstPos.y, obstPos.y + 2), obstPos.z);
            human.transform.position = humanPos;
            human.transform.parent = humansHolder.transform;
        }
    }

        private void spawnFillers(Vector3 obstPos){
        if(drawChances(45, 100)){
            GameObject filler = (GameObject) Instantiate(fillerPrefab);
            Vector3 fillerPos = new Vector3( Random.Range(obstPos.x - 2, obstPos.x + 3), Random.Range(obstPos.y, obstPos.y + 2), obstPos.z);
            filler.GetComponent<SpriteRenderer>().sprite = fillerSprites[Random.Range(0, fillerSprites.Length - 1)]; 
            filler.transform.position = fillerPos;
            filler.transform.parent = fillersHolder.transform;
        }
    }

    
    private bool drawChances(int chance, int limit){
        return Random.Range(0, limit) < chance;
    }


}
