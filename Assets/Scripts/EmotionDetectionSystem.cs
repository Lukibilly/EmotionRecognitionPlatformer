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
        string ferpath = "C:\\Users\\631lh\\Documents\\UNI\\Neuro-Usability\\Facial-emotion-recognition\\dist\\live_cam_predict\\live_cam_predict.exe";
        ProcessStartInfo ferInfo = new ProcessStartInfo();
        ferInfo.CreateNoWindow = true;
        ferInfo.UseShellExecute = true;
        ferInfo.FileName = ferpath;
        ferInfo.WorkingDirectory = "C:\\Users\\631lh\\Documents\\UNI\\Neuro-Usability\\Facial-emotion-recognition\\dist\\live_cam_predict";
        fer = Process.Start(ferInfo);
    }
    
}
