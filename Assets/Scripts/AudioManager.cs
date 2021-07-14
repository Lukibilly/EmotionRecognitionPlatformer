using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    void Awake(){instance = this;}

    public AudioClip sfx_looselife;
    public AudioClip defaultMusic;
    public AudioClip happyMusic;
    public AudioClip psytranceMusic;
    public AudioClip holyMusic;
    public AudioClip boringMusic;

    GameObject currentMusicObject;
    string currentlyPlaying = "Nothing";
    
    public GameObject soundObject; //soundObject Prefab

    public void playSFX(string sfxName){
        switch(sfxName){
            case "sfx_looselife":
                sfxObjectCreation(sfx_looselife);
                break;
            default:
                break;
        }
    }
    void sfxObjectCreation(AudioClip clip){
        GameObject newObject = Instantiate(soundObject, transform);
        newObject.GetComponent<AudioSource>().clip = clip;
        newObject.GetComponent<AudioSource>().Play();
    }
    public void PlayMusic(string musicName){
        if(currentlyPlaying.Equals(musicName)) return;
        currentlyPlaying = musicName;
        switch(musicName){
            case "default":                
                musicObjectCreation(defaultMusic);                
                break;
            case "happy":                
                musicObjectCreation(happyMusic);
                break;
            case "psytrance":                
                musicObjectCreation(psytranceMusic);
                break;
            case "holy":                
                musicObjectCreation(holyMusic);
                break;
            case "boring":                
                musicObjectCreation(boringMusic);
                break;
            default:
                break;
        }
    }
    public void startMusic(){
        if(currentMusicObject) currentMusicObject.GetComponent<AudioSource>().Play();
    }
    void musicObjectCreation(AudioClip clip){
        bool newmusic = false;
        if(currentMusicObject) currentMusicObject.AddComponent<FadeSound>();
        else newmusic = true;
        currentMusicObject = Instantiate(soundObject, transform);
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        currentMusicObject.GetComponent<AudioSource>().loop = true;
        if(newmusic) currentMusicObject.GetComponent<AudioSource>().Play();
        
    }
}
