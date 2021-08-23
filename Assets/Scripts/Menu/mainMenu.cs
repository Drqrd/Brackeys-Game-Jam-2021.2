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
    [SerializeField]
    private Sprite[] buttonSelectables;
    private int buttonSelected = 0; //0= default, 1 = play, 2= stats, 3 = exit, 
    private SpriteRenderer selectedSprite;


    private void Start(){
        selectedSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        this.handleButton_selections();
        if(Input.GetKeyDown(KeyCode.Return))   
            this.buttonClicks();
    }


    private void handleButton_selections(){
        if(Input.GetKeyDown(KeyCode.W))  buttonSelected--;
        else if(Input.GetKeyDown(KeyCode.S)) buttonSelected++;
        if(buttonSelected < 0 ) buttonSelected = 3;
        else if(buttonSelected > 3) buttonSelected = 1;
        selectedSprite.sprite = buttonSelectables[buttonSelected];
    }

    private void buttonClicks(){
        print(1);
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
    }

    private void loadStats(){

    }

    private void exitGame(){

    }
}
