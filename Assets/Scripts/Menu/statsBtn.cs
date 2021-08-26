using UnityEngine;

public class statsBtn : MonoBehaviour
{
    [SerializeField] private int statBtnID; //0 = back, 1= story, 2=left, 3= right
    [SerializeField] private Sprite[] btnSprites; //0= noFocus, 1= onFocus
    [SerializeField] private GameObject[] chapters;
    [SerializeField] private GameObject[] storyTooglers;
    
    private statsLoader statsBar; 
    private int currentChapter;


    private void Start(){
        statsBar = transform.parent.transform.parent.GetComponent<statsLoader>();
        
    }



    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().sprite = btnSprites[1];
    }

    
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = btnSprites[0];
    }


    private void OnMouseDown()
    {
        switch(statBtnID){
            case 0:
                backBtn();
                break;
            case 1:
                storyBtn();
                break;
            default:
                storyToogle(statBtnID);
                break;
        }
    }


    private void backBtn(){
        statsBar.menuRef.SetActive(true);
        statsBar.canvasRef.SetActive(false);
        statsBar.gameObject.SetActive(false);
        statsBar.storyBtn.SetActive(true);
        foreach(GameObject storyToogler in storyTooglers){
            storyToogler.SetActive(false);
        }
        currentChapter = 0;
    }


    private void storyBtn(){
        statsBar.canvasRef.SetActive(true);
        statsBar.canvasStats.SetActive(false);
        statsBar.storyRef.SetActive(true);
        this.gameObject.SetActive(false);
        foreach(GameObject storyToogler in storyTooglers){
            storyToogler.SetActive(true);
        }
        chapters[0].SetActive(true);
        currentChapter = 0;
    }

    private void storyToogle(int moveID){ //left =2, right = 3;       
        if(moveID == 2){
            currentChapter--;
        }else{
            currentChapter++;
        }
        if(currentChapter <= 0) currentChapter = 0;
        if(currentChapter >= chapters.Length) currentChapter = chapters.Length - 1;     
        foreach(GameObject chapter in chapters){
            chapter.SetActive(false);
        }
        chapters[currentChapter].SetActive(true);
    }



}
