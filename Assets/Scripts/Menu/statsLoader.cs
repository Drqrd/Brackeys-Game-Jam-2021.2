using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;


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
///    2. Handles saving the game after player dies


/// </summary>




public class statsLoader : MonoBehaviour
{
    public GameObject menuRef;
    public GameObject canvasRef;
    public GameObject canvasStats;
    public GameObject storyRef;
    public GameObject storyBtn;
    public GameData gameData;

    private ArrayList gameData_list;


    
    private void Start()
    {
        this.clearStory_loadStats();
    }


    public void clearStory_loadStats(){
        canvasStats.SetActive(true);
        storyRef.SetActive(false);
        this.load_gameData();
        for(int dataIndex = 0; dataIndex < canvasStats.transform.childCount; dataIndex++){
            GameObject gameStat = canvasStats.transform.GetChild(dataIndex).gameObject; 
            GameObject gameStatBody = gameStat.transform.GetChild(1).gameObject;          
            if(gameStat.name == "travelled") gameStatBody.GetComponent<Text>().text = gameData_list[dataIndex].ToString() + "m"; 
            else if(gameStat.name == "runtime") gameStatBody.GetComponent<Text>().text = gameData_list[dataIndex].ToString() + "s"; 
            else gameStatBody.GetComponent<Text>().text = gameData_list[dataIndex].ToString();  
            
        }
    }


    private void load_gameData(){
        string saveString = File.ReadAllText(Application.dataPath + "/Resources/Data/gamedata.json");
        gameData = JsonUtility.FromJson<GameData>(saveString);
        gameData_list = new ArrayList();
        gameData_list.Add(gameData.ID); gameData_list.Add(gameData.travelled);
        gameData_list.Add(gameData.killed); gameData_list.Add(gameData.died);
        gameData_list.Add(gameData.destroyed); gameData_list.Add(gameData.runtime);
    }

    private void save_gameData(){
        string gamedataJson = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.dataPath + "/Resources/Data/gamedata.json", gamedataJson);
    }


    /*
         object: string(jsonFormat) = string json = JsonUtility.ToJson(gameData);
         string(jsonFormat): object = GameData loadedGameData = JsonUtility.FromJson<GameData>(json);
    */
}
