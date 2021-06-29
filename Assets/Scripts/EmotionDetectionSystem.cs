using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

public class EmotionDetectionSystem : MonoBehaviour
{
    private Process fer;
    private int nextEmotionCheck = 1;
    private int nextGameChange = 10;
    public int emotion_check_frequency = 1;
    public int game_change_frequency = 10;
    private List<string> emotionList = new List<string>();

    void Update(){
        if(Time.time>=nextEmotionCheck){
            nextEmotionCheck=Mathf.FloorToInt(Time.time)+emotion_check_frequency;
            string currentEmotion = getCurrentEmotion();
            if(currentEmotion!="NOEMOTION") emotionList.Add(currentEmotion);
        }
        if(Time.time>=nextGameChange){
            nextGameChange=Mathf.FloorToInt(Time.time)+game_change_frequency;
            changeGame();
        }        
    }

    void Start()
    {
        startFer();
    }
    void OnApplicationQuit(){
        fer.Kill();
    }
    void startFer(){
        string folderPath = Application.dataPath + "\\EmotionDetectionExe\\Facial-emotion-recognition\\dist\\live_cam_predict\\";
        string exePath = folderPath + "\\live_cam_predict.exe";
        ProcessStartInfo ferInfo = new ProcessStartInfo();
        ferInfo.CreateNoWindow = false;
        ferInfo.UseShellExecute = true;
        ferInfo.FileName = exePath;
        ferInfo.WorkingDirectory = folderPath;
        fer = Process.Start(ferInfo);
    }
    string getCurrentEmotion(){
        var list = new List<string>{"Positive","Negative","Neutral"};
        int index = UnityEngine.Random.Range(0,3);
        return list[index];
    }
    void changeGame(){
        UnityEngine.Debug.Log("I change the game according to last emotion");
    }
}
