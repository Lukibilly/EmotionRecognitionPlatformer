using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float timeValue = 300;
    public Text timeText;
    public string timeString = "";
    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.gameStarted){
            if(timeValue>0){
                timeValue -= Time.deltaTime;
            }else{
                timeValue=0;
            }
            DisplayTime(timeValue);
            if(timeValue==0) GameController.instance.finishGame(false);
        }
        
    }
    void DisplayTime(float timeToDisplay){
        if(timeToDisplay < 0){
            timeToDisplay=0;
        }else if(timeToDisplay>0){
            timeToDisplay += 1;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay/60);
        float seconds = Mathf.FloorToInt(timeToDisplay%60);
        timeString = string.Format("{0:00}:{1:00}",minutes,seconds);
        timeText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
}
