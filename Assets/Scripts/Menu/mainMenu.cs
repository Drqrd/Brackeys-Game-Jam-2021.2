using UnityEngine;



/// <summary>
/// 
/// Last Modified: 8/23/21
/// 
/// Class: Game Menu (UI)
///  
/// Author: Shubham Tiwary
///
/// Description:
///    The Main Menu controller.
///    Features Include:
///     1. Play - Spawns player and starts moving
///     2. Stats - Shows highscore and Other stats
///     3. Exits game
/// 
/// </summary>

public class mainMenu : MonoBehaviour
{
    [SerializeField]  private Sprite[] buttonSelectables;
    [SerializeField] private GameObject statsHolder;
    [SerializeField] private GameObject UICanvas; 

    private int buttonSelected = 1; //0= default, 1 = play, 2= stats, 3 = exit, 
    private SpriteRenderer selectedSprite;

    // reference to pause
    private Main gameRef;
    private Player playerRef;
    
    public audioManager audioRef;
    public const string CLICK_AUDIO = "mouseClick"; 

    

    private void Start(){
        selectedSprite = gameObject.GetComponent<SpriteRenderer>();
        gameRef = GameObject.Find("GameController").GetComponent<Main>();
        playerRef = GameObject.Find("Player").GetComponent<Player>();
        audioRef = GameObject.Find("AudioManager").GetComponent<audioManager>();
    }

    private void Update()
    {
        this.handleButton_selections();
        if(Input.GetKeyDown(KeyCode.Return)){
            audioRef.playAudio(CLICK_AUDIO);
            this.buttonClicks();
        } 
    }


    private void handleButton_selections(){
        if(Input.GetKeyDown(KeyCode.W)){
            audioRef.playAudio(CLICK_AUDIO);
            buttonSelected--;
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            audioRef.playAudio(CLICK_AUDIO);
            buttonSelected++;
        }
        if(buttonSelected < 0 ) buttonSelected = 3;
        else if(buttonSelected > 3) buttonSelected = 1;
        selectedSprite.sprite = buttonSelectables[buttonSelected];
    }

    private void buttonClicks(){
        switch(buttonSelected){
            case 1:
                this.startGame();
                break;
            case 2:
                this.loadStats();
                break;
            case 3:
                this.exitGame();
                break;
            default:
                break;
        }
    }

    private void startGame(){
        gameObject.SetActive(false);
        GameObject.Find("Player").GetComponent<SpriteRenderer>().enabled = true;
        gameRef.paused = false;
        statsHolder.SetActive(false);
        gameRef.distanceStat.SetActive(true);
        gameRef.scoreStat.SetActive(true);
    }

    private void loadStats(){
        gameObject.SetActive(false);
        UICanvas.SetActive(true);
        statsHolder.SetActive(true);
        statsHolder.GetComponent<statsLoader>().clearStory_loadStats();
    }

    private void exitGame(){
        Application.Quit();
    }
}
