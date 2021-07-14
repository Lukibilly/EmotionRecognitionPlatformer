using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public int playerDeaths;    
    void Awake(){instance = this; playerDeaths= 0;}

    public bool gameStarted = false;
    public GameObject loadingPanel;
    public TextMeshProUGUI  loadingcontinue;
    public Button continuebutton;
    private bool loadedModel = false;
    public TextMeshProUGUI gameMessage;
    public bool displayingMessage = false;
    public float messageDisplayTime = 5f;
    private float initialMessageDisplayTime;

    public Image image;
    public bool displayingImage = false;
    public float imageDisplayTime = 5f;
    private float initialImageDisplayTime;

    public Countdown countdown;
    public FinishFlag finishFlag;
    public TextMeshProUGUI finishMessage;
    public Button finishButton;
    public EmotionDetectionSystem emotionDetectionSystem;


    void Update(){
        if(displayingMessage){
            if(initialMessageDisplayTime+messageDisplayTime < Time.time){
                gameMessage.gameObject.SetActive(false);
                displayingMessage = false;
            }
        }
        if(displayingImage){
            if(initialImageDisplayTime+imageDisplayTime < Time.time){
                image.gameObject.SetActive(false);
                displayingImage = false;
            }
        }
    }
    
    public void startGame(){
        loadedModel = true;
        loadingcontinue.GetComponent<TextMeshProUGUI>().SetText("CONTINUE");
        continuebutton.gameObject.SetActive(true);
    }
    public void clickContinue(){
        if(loadedModel){
            loadingPanel.SetActive(false);
            gameStarted = true;
            AudioManager.instance.PlayMusic("default");
        }
    }
    public void displayMessage(string message, float displayTime){
        gameMessage.GetComponent<TextMeshProUGUI>().SetText(message);        
        gameMessage.gameObject.SetActive(true);
        messageDisplayTime = displayTime;
        initialMessageDisplayTime = Time.time;
        displayingMessage = true;
    }
    public void displayImage(string imagename, float displayTime){
        image.gameObject.SetActive(true);
        imageDisplayTime = displayTime;
        initialImageDisplayTime = Time.time;
        displayingImage = true;
    }

    public void changeColor(){
        var background = GameObject.Find("Background");
        background.GetComponent<SpriteRenderer>().color = new Color(255,0,0);
        var ground = GameObject.Find("Ground");
        ground.GetComponent<Tilemap>().color = new Color(255,0,0);
    }
    public void resetColor(){
        var background = GameObject.Find("Background");
        background.GetComponent<SpriteRenderer>().color = new Color(255,255,255);
        var ground = GameObject.Find("Ground");
        ground.GetComponent<Tilemap>().color = new Color(255,255,255);
    }
    public void finishGame(bool reachedFinishFlag){
        gameStarted = false;
        var player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().runSpeed = 0;
        player.GetComponent<PlayerMovement>().setCanMove(false);
        if(reachedFinishFlag) finishMessage.GetComponent<TextMeshProUGUI>().SetText("Congratulations! You reached the end with "+countdown.timeString+" left!");
        else{
            finishMessage.GetComponent<TextMeshProUGUI>().SetText("Time is up! You completed "+Mathf.FloorToInt(100*finishFlag.maxDistance/467)+"% of the game!");
        }
        finishMessage.gameObject.SetActive(true);
        finishButton.gameObject.SetActive(true);
    }
    public void goToMainMenu(){
        emotionDetectionSystem.killfer();
        SceneManager.LoadScene("Menu");
    }
}
