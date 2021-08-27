using UnityEngine;

public class backgroundSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] backgroundLayers;
    private Main gameRef;

     private float spawnDistance_break = 50f;
    private float lastDist_point = 0f;
    private Vector3[] furthestPoints;




    
    private void Start()
    {
        gameRef = GameObject.Find("GameController").GetComponent<Main>();
        furthestPoints = new Vector3[4] {Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero};
    }

    
    private void LateUpdate()
    {
        this.measureSpawn_breaks();
    }



    private void measureSpawn_breaks(){
        if( gameRef.distanceTraveled - lastDist_point >= spawnDistance_break ){
            this.spawnBackgrounds();
            this.disposeWaste_background();
            lastDist_point = gameRef.distanceTraveled;
        }
    }


    private void spawnBackgrounds(){
        findFurthestBackground_foreach();
        for(int spawnIndex = 0; spawnIndex < furthestPoints.Length; spawnIndex++){
            GameObject spawnedbackground = Instantiate(backgroundLayers[spawnIndex]);
            spawnedbackground.transform.position = new Vector3(furthestPoints[spawnIndex].x + spawnDistance_break, furthestPoints[spawnIndex].y);
            spawnedbackground.transform.parent = this.gameObject.transform;
        }
        
    }

    private void findFurthestBackground_foreach(){
        for(int bgChildIndex = 0; bgChildIndex < transform.childCount; bgChildIndex++){
            GameObject bgChild = transform.GetChild(bgChildIndex).gameObject;
            if(takeLowestIndexMultiple(bgChildIndex) == bgChild.GetComponent<parallax>().backgroundID){
                if(furthestPoints[takeLowestIndexMultiple(bgChildIndex)].x < bgChild.transform.position.x){
                    furthestPoints[takeLowestIndexMultiple(bgChildIndex)] =  bgChild.transform.position;
                }
            }   
        }
    }


    private void disposeWaste_background(){
        for(int bgChildIndex = 0; bgChildIndex < transform.childCount; bgChildIndex++){
            GameObject bgChild = transform.GetChild(bgChildIndex).gameObject;
            if(bgChild.transform.position.x < Camera.main.transform.position.x - (spawnDistance_break * 2)){
                Destroy(bgChild);
            }
        }
    }



    //takes any number, subtarcts four till it falls in 0,1,2,3 range(IDs)
    private int takeLowestIndexMultiple(int highestNum){
        int lowestNum = highestNum;
        while(lowestNum >= 3){
            lowestNum -= 4;
        }
        if(lowestNum == -1)
            return 3;
        return lowestNum;
    }


}
