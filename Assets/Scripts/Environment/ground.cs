using UnityEngine;

public class ground : MonoBehaviour
{

    //COMPONENTS
    private Camera mainCam;   
    private int groundFocus; //0= left of cam, 1= inside cam, 2= right of cam 
    private float cameraWidth;
    private float groundObject_width;


    void Start()
    {
        mainCam = Camera.main;
        cameraWidth = (mainCam.orthographicSize * 2) * (mainCam.aspect);
        groundFocus = setGround_focus();
        groundObject_width = GetComponent<Collider>().bounds.size.x;       
    }


    private int setGround_focus(){
        if( (transform.position.x + groundObject_width / 2) < (mainCam.transform.position.x - cameraWidth/2) ) 
            return 0;
        else if( (transform.position.x - groundObject_width / 2) > (mainCam.transform.position.x + cameraWidth/2) )
            return 2;
        return 1;
    }

    
}
