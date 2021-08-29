using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Player playerRef;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(playerRef.transform.position.x, transform.position.y, transform.position.z);
    }
}
