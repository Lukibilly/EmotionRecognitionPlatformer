using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;    
    void Awake(){instance = this;}

    public bool gameStarted = false;
    public GameObject loadingPanel;
    public TextMeshProUGUI  loadingcontinue;
    public Button continuebutton;
    //private bool clickedContinue = false;
    private bool loadedModel = false;
    
    public void startGame(){
        loadedModel = true;
        loadingcontinue.GetComponent<TextMeshProUGUI>().SetText("CONTINUE");
        continuebutton.gameObject.SetActive(true);
    }
    public void clickContinue(){
        //clickedContinue = true;
        if(loadedModel){
            loadingPanel.SetActive(false);
            gameStarted = true;
        }
    }
}
