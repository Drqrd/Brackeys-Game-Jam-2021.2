using UnityEngine;
using System.Collections.Generic;
using System.IO;


/// <summary>
/// 
/// Last Modified: 8/25/21
/// 
/// Class: Game Stats (UI)
///  
/// Author: Shubham Tiwary
///
/// Description:
///    1. Handles Loading player states from external stored data and pasting onto the stats bar
/// 
/// </summary>




public class statsLoader : MonoBehaviour
{
    public GameObject menuRef;
    public GameObject canvasRef;
    public GameObject canvasStats;
    public GameObject storyRef;
    public GameObject storyBtn;


    
    private void Start()
    {
        
    }

    
    private void Update()
    {
        
    }


    public void clearStory_loadStats(){
        canvasStats.SetActive(true);
        storyRef.SetActive(false);
    }

    
}
