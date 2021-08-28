using UnityEngine;
using System;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Reflection;



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


    public GameObject playerStatImage;
    public Sprite[] playerStatImages;

    private ArrayList gameData_list;




    public void clearStory_loadStats(bool endGame = false){
        canvasStats.SetActive(true);
        storyRef.SetActive(false);
        if(!endGame)
            playerStatImage.GetComponent<SpriteRenderer>().sprite = playerStatImages[0];
        this.load_gameData(endGame);
        for(int dataIndex = 0; dataIndex < canvasStats.transform.childCount; dataIndex++){
            GameObject gameStat = canvasStats.transform.GetChild(dataIndex).gameObject; 
            GameObject gameStatBody = gameStat.transform.GetChild(1).gameObject;       
            if(gameStat.name == "travelled") gameStatBody.GetComponent<Text>().text = gameData_list[dataIndex].ToString() + "m"; 
            else if(gameStat.name == "runtime") gameStatBody.GetComponent<Text>().text = gameData_list[dataIndex].ToString() + "s"; 
            else gameStatBody.GetComponent<Text>().text = gameData_list[dataIndex].ToString();  
        }
    }

    public void loadEndGame_stats(){
        canvasRef.SetActive(true);
        canvasStats.SetActive(true);
        storyBtn.SetActive(false);
        playerStatImage.GetComponent<SpriteRenderer>().sprite = playerStatImages[1];
        this.load_gameData(endGame: true);
        this.clearStory_loadStats(endGame: true);
        this.endGame_save();
    }

    private void load_gameData(bool endGame = false){
        if(!endGame){
            string saveString = File.ReadAllText(Application.dataPath + "/Resources/Data/gamedata.json");
            gameData = JsonUtility.FromJson<GameData>(saveString);
        }   
        gameData_list = new ArrayList();
        gameData_list.Add(gameData.ID); gameData_list.Add(gameData.travelled);
        gameData_list.Add(gameData.killed); gameData_list.Add(gameData.score);
        gameData_list.Add(gameData.destroyed); gameData_list.Add(gameData.runtime);
    }

    private void save_gameData(){
        string gamedataJson = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.dataPath + "/Resources/Data/gamedata.json", gamedataJson);
    }

    public void setGameData_stats(float travelled, int killed, int destroyed, float runtime, int points){
        gameData = new GameData();
        gameData.ID = "Enyor-15";
        gameData.travelled =  (int) travelled;
        gameData.killed = killed;
        gameData.score = points;
        gameData.destroyed = destroyed;
        gameData.runtime = runtime;
    }


    private void endGame_save(){
        GameData currentGamedata = DataUtility.copyGameData(gameData);  
        load_gameData();
        gameData.travelled += currentGamedata.travelled;
        gameData.killed += currentGamedata.killed;
        gameData.score += currentGamedata.score;
        gameData.destroyed += currentGamedata.destroyed;
        gameData.runtime += currentGamedata.runtime;
        save_gameData();  
    }


    /*
         object: string(jsonFormat) = string json = JsonUtility.ToJson(gameData);
         string(jsonFormat): object = GameData loadedGameData = JsonUtility.FromJson<GameData>(json);
    */
}
