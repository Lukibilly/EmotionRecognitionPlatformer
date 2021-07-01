using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class EmotionDetectionSystem : MonoBehaviour
{
    private Process fer;

    private double nextEmotionCheck = 1;
    private double nextGameChange = 15;
    public double emotion_check_frequency = 0.5;
    public double game_change_frequency = 11;

    private List<string> emotionList = new List<string>();
    private string lastEmotionChange = "NONE";

    private string folderPath;

    void Update(){

        if(Time.time>=nextEmotionCheck){
            nextEmotionCheck=Time.time+emotion_check_frequency;
            string currentEmotion = getCurrentEmotion();
            if(currentEmotion!="NOEMOTION"){
                emotionList.Add(currentEmotion);
                UnityEngine.Debug.Log(currentEmotion);
            }
        }

        if(Time.time>=nextGameChange){
            nextGameChange=Time.time+game_change_frequency;
            if(getCurrentEmotion()!="NOEMOTION") changeGame();
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
        folderPath = Application.dataPath + "\\EmotionDetectionExe\\live_cam_predict\\";
        string emotionFilePath = folderPath + "\\emotion_file.txt";
        File.WriteAllText(emotionFilePath,"NOEMOTION\n");    
        string exePath = folderPath + "\\live_cam_predict.exe";
        ProcessStartInfo ferInfo = new ProcessStartInfo();
        ferInfo.CreateNoWindow = true;
        ferInfo.UseShellExecute = false;
        ferInfo.FileName = exePath;
        ferInfo.WorkingDirectory = folderPath;
        fer = Process.Start(ferInfo);
    }

    string getCurrentEmotion(){
        string emotionFilePath = folderPath + "\\emotion_file.txt";
        string currentEmotion = ReadLines(emotionFilePath).Last();
        return currentEmotion;
    }

    void changeGame(){
        UnityEngine.Debug.Log("I change the game according to last emotion");
        // GET CURRENT EMOTION FROM LAST ELEMENTS IN LIST
        string currentEmotion = "Positive"; // THIS IS JUST A TEMPLATE
        // IF ITS A NEW EMOTION
        if(lastEmotionChange.Equals(currentEmotion))applyRegularChanges(currentEmotion);
        // IF LASTEMOTION = CURRENTEMOTION
        else applyStagnateChanges(currentEmotion);
        // UPDATE LASTEMOTION
        lastEmotionChange = currentEmotion;
    }

    void applyRegularChanges(string currentEmotion){        
        // NEUTRAL
            // playerVelocitylevel = NORMAL
            // fallingEnemyRate = NORMAL (falling enemies die when they fall in hole)
            // music = DEFAULT
        // POSITIVE
            // playerVelocitylevel = HIGH
            // fallingEnemyRate = HIGH // (falling enemies die when they fall in hole)
            // music = CREEPY/TENSE
    }
    void applyStagnateChanges(string currentEmotion){
        // NEUTRAL -> NEUTRAL ------ MAYBE DIFFERENT OPTIONS CHOSEN AT RANDOM
            // message: What's up with that lousy face? Are you not enjoying this?
            // change player form to be tiny
            // playerVelocitylevel = MEDIUM-HIGH
            // music = WEIRD WTF
        // POSITIVE -> POSITIVE
            // message: Are you mocking the gods? Face your punishment.
            // fallingEnemyRate = EXTREME
            // music = HARD PSYTRANCE
            // background+player color = red
            // enemy color = black
            // playerVelocitylevel = HIGH (ALREADY IN REGULAR CHANGES)        
    }
    public static IEnumerable<string> ReadLines(string path)
{
    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan))
    using (var sr = new StreamReader(fs))
    {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            yield return line;
        }
    }
}
}
