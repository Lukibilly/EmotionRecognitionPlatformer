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
    private GameObject player;
    private GameObject spawner;
    public int neededNegative = 2;
    public int neededPositive = 3;
    public int normalRunSpeed = 20;
    public int mediumRunSpeed = 23;
    public int highRunSpeed = 26;
    private double nextEmotionCheck = 1;
    private double nextGameChange = 15;
    public double emotion_check_frequency = 0.5;
    public double game_change_frequency = 11;

    private List<string> emotionList = new List<string>();
    private string lastEmotionChange = "NONE";

    private string folderPath;

    private bool psytrancemode;
    private bool holymode;
    public float bonusJumpPower = 3;

    void Update(){

        if(Time.time>=nextEmotionCheck){
            nextEmotionCheck=Time.time+emotion_check_frequency;
            string currentEmotion = getCurrentEmotion();
            if(currentEmotion!="NOEMOTION"){
                if(GameController.instance.gameStarted==false) GameController.instance.startGame();
                emotionList.Add(currentEmotion);
            }
        }

        if(Time.time>=nextGameChange){
            nextGameChange=Time.time+game_change_frequency;
            if(getCurrentEmotion()!="NOEMOTION" && GameController.instance.gameStarted) changeGame();
        }        
    }

    void Start()
    {
        startFer();
        player = GameObject.Find("Player");
        spawner = GameObject.Find("EnemySpawner");
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

    public string getCurrentEmotion(){
        string emotionFilePath = folderPath + "\\emotion_file.txt";
        string currentEmotion = ReadLines(emotionFilePath).Last();
        return currentEmotion;
    }

    void changeGame(){
        string logstring = "Changing game:";
        
        // GET CURRENT EMOTION FROM LAST ELEMENTS IN LIST
        string currentAvgEmotion = avgEmotion(); // THIS IS JUST A TEMPLATE
        logstring += currentAvgEmotion;
        //UnityEngine.Debug.Log(logstring);
        
        
        // IF ITS SAME EMOTION TWICE IN A ROW
        if(lastEmotionChange.Equals(currentAvgEmotion)) applyStagnateChanges(currentAvgEmotion);
        else applyRegularChanges(currentAvgEmotion);
        
        // UPDATE LASTEMOTION
        lastEmotionChange = currentAvgEmotion;
    }

    void applyRegularChanges(string currentAvgEmotion){
        // NEUTRAL
        if(psytrancemode){
            GameController.instance.resetColor();
            psytrancemode = false;
        }
        if(holymode){
            player.GetComponent<PlayerMovement>().jumpPower -= bonusJumpPower;
            holymode = false;
        }
        player.GetComponent<PlayerMovement>().runSpeed = normalRunSpeed;
        if(currentAvgEmotion=="Negative"){
            GameController.instance.displayMessage("So you can't handle my world...",8);
            spawner.GetComponent<EnemySpawner>().setEnemySpawnRate(EnemySpawner.SpawnRate.NONE);
            player.GetComponent<PlayerMovement>().runSpeed = normalRunSpeed;
            GameController.instance.displayImage("boring",8);
            AudioManager.instance.PlayMusic("boring");            
        }
        if(currentAvgEmotion=="Neutral"){
            player.GetComponent<PlayerMovement>().runSpeed = normalRunSpeed;
            spawner.GetComponent<EnemySpawner>().setEnemySpawnRate(EnemySpawner.SpawnRate.NORMAL);
            AudioManager.instance.PlayMusic("default");
        }        
        // POSITIVE
        if(currentAvgEmotion=="Positive"){
            player.GetComponent<PlayerMovement>().runSpeed = mediumRunSpeed;
            spawner.GetComponent<EnemySpawner>().setEnemySpawnRate(EnemySpawner.SpawnRate.HIGH);
            AudioManager.instance.PlayMusic("happy");
        }
    }

    void applyStagnateChanges(string currentAvgEmotion){
        if(currentAvgEmotion == "Neutral"){
            if(!psytrancemode){
                GameController.instance.displayMessage("Is my world boring to you? Face Gods Wrath!",10);
                spawner.GetComponent<EnemySpawner>().setEnemySpawnRate(EnemySpawner.SpawnRate.EXTREME);
                player.GetComponent<PlayerMovement>().runSpeed = highRunSpeed;
                GameController.instance.changeColor();
                AudioManager.instance.PlayMusic("psytrance");
                psytrancemode = true;
            }
        }
        if(currentAvgEmotion == "Positive"){
            if(!holymode){
                GameController.instance.displayMessage("Have this power to enjoy more of my world!",10);
                player.GetComponent<PlayerMovement>().runSpeed = highRunSpeed;
                player.GetComponent<PlayerMovement>().jumpPower += bonusJumpPower;
                AudioManager.instance.PlayMusic("holy");
                holymode = true;
            }            
        }        
    }

    private string avgEmotion(){
        int numElements = Math.Min(emotionList.Count(),20);
        int negativeCount = 0;
        int neutralCount = 0;
        int positiveCount = 0;
        for(int i=0;i<numElements;i++){
            if(emotionList[emotionList.Count()-1-i] == "positive") positiveCount++;
            if(emotionList[emotionList.Count()-1-i] == "negative") negativeCount++;
            if(emotionList[emotionList.Count()-1-i] == "neutral") neutralCount++;
        }
        if(negativeCount>=neededNegative) return "Negative";
        if(positiveCount>=neededPositive) return "Positive";        
        return "Neutral";
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
    public void killfer(){
        fer.Kill();
    }
}
