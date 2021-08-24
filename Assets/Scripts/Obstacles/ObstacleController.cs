using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private Main gameRef;

    private void Start()
    {
        gameRef = GameObject.Find("GameController").GetComponent<Main>();
    }

    private void FixedUpdate()
    {
        foreach (Transform child in transform)
        {
            if (child.position.x < gameRef.leftBound)
            {
                child.GetComponent<Area>().Teleport();
            }
        }
    }
}
