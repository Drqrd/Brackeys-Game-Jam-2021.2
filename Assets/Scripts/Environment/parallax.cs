using UnityEngine;

/// <summary>
/*
/// Last Modified: 8/24/21
/// 
/// Class: Parallax
///  
/// Author: Shubham Tiwary
///
/// Description:
///    Handles the background movement transition by creating a smooth parallax effect
/// 
*/ 
/// </summary>



public class parallax : MonoBehaviour
{

    public int backgroundID;
    [SerializeField]  private float parallaxLevel;
    public GameObject nextBackground;
    private Main gameRef;

    private Transform camTransform;
    private Vector3 lastCamPosition;
    public float spriteWidth;


    private float distanceBreak;
    private float lastDist_point = 0f;







    private void Start()
    {
      spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
      distanceBreak = spriteWidth;
      camTransform = Camera.main.transform;
      lastCamPosition = camTransform.position;
      gameRef = GameObject.Find("GameController").GetComponent<Main>();
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = camTransform.position - lastCamPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxLevel, 0f);
        lastCamPosition = camTransform.position;      
        this.setDistance_travelledFrom_lastPoint();
    }




    private void setDistance_travelledFrom_lastPoint(){   
        if( gameRef.distanceTraveled - lastDist_point >= distanceBreak ){
            lastDist_point = gameRef.distanceTraveled;
        }       
    }



    



}
