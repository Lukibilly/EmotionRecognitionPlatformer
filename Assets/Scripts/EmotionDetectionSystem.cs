using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;

public class EmotionDetectionSystem : MonoBehaviour
{
    Process fer;
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
    
}
