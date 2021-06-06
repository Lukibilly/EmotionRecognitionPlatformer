using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    void Awake(){instance = this;}

    public AudioClip sfx_looselife;
    //public AudioClip music_start;

    GameObject currentMusicObject;
    
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
        switch(musicName){
            case "music_start":
                //musicObjectCreation(music_start);
                break;
            default:
                break;
        }
    }
    void musicObjectCreation(AudioClip clip){
        if(currentMusicObject) Destroy(currentMusicObject);
        currentMusicObject = Instantiate(soundObject, transform);
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        currentMusicObject.GetComponent<AudioSource>().loop = true;
        currentMusicObject.GetComponent<AudioSource>().Play();
    }
}
