using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    private float length, startPos;
    [SerializeField]
    private GameObject mainCam;
    [SerializeField]
    private float parallaxLevel;


    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    
    private void FixedUpdate()
    {
        float posDifference = (mainCam.transform.position.x * (1 - parallaxLevel));
        float cameraDistance = (mainCam.transform.position.x * parallaxLevel);

        transform.position = new Vector3(startPos + cameraDistance, transform.position.y, transform.position.z);

        if(posDifference > startPos + length) startPos += length;
        else if(posDifference < startPos - length) startPos -= length;
    }
}
