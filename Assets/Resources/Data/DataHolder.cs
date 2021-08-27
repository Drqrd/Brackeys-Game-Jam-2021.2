

public class GameData{
    public string ID;
    public int travelled;
    public int killed;
    public int died;
    public int destroyed;
    public float runtime;

}


public static class DataUtility 
{
    public static GameData copyGameData(GameData source)
    {
        GameData newGameData = new GameData();
        newGameData.ID = source.ID;
        newGameData.travelled = source.travelled;
        newGameData.killed = source.killed;
        newGameData.died = source.died;
        newGameData.destroyed = source.destroyed;
        newGameData.runtime = source.runtime;
        return newGameData;
    }


    public static string printGameData(GameData gameData){
            return "ID : "  + gameData.ID + 
                "     Travelled: " + gameData.travelled + 
                "     Killed: " + gameData.killed + 
                "     Died: " + gameData.died + 
                "     Destroyed: " + gameData.destroyed + 
                "     Runtime: " + gameData.runtime;
    }
}
