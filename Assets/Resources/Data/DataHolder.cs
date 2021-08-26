using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public GameData[] gameData;
}

public class GameData{
    public string ID;
    public int travelled;
    public int killed;
    public int died;
    public int destroyed;
    public string runtime;
}
