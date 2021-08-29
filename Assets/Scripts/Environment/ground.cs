using UnityEngine;

/// <summary>
/*
/// Last Modified: 8/24/21
/// 
/// Class: Ground
///  
/// Author: Shubham Tiwary
///
/// Description:
///    Ground logic for a single tile: mainly checks where the ground's pos is i.e, behind, on focus or after the camera
/// 
*/ 
/// </summary>



public class ground : MonoBehaviour
{

    //COMPONENTS
    public Camera mainCam;   
    public int groundFocus; //0= left of cam, 1= inside cam, 2= right of cam 
    private float cameraWidth;
    private float groundObject_width;

    void Start()
    {
        mainCam = Camera.main;
        cameraWidth = (mainCam.orthographicSize * 2) * (mainCam.aspect);
        groundFocus = setGround_focus();
        groundObject_width = 1f; 
    }

    /*-------------------------------------*/
    /* Justin D'Errico edits for bugfixing */
    // Will execute every time a new object is loaded
    void Awake()
    {
        mainCam = Camera.main;
        cameraWidth = (mainCam.orthographicSize * 2) * (mainCam.aspect);
        groundFocus = setGround_focus();
        groundObject_width = 1f;
    }
    /*-------------------------------------*/


    public int setGround_focus(){
        if( (transform.position.x + groundObject_width / 2) < (mainCam.transform.position.x - cameraWidth/2) ) 
            return 0;
        else if( (transform.position.x - groundObject_width / 2) > (mainCam.transform.position.x + cameraWidth/2) )
            return 2;
        return 1;
    }

    
    
}
