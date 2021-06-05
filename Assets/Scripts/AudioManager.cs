using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    void Awake(){instance = this;}

    public AudioClip sfx_attack, sfx_run;
    public AudioClip music_start;

    public GameObject currentMusicObject;
    
    public GameObject soundObject; //soundObject Prefab

    public void playSFX(string sfxName){
        switch(sfxName){
            case "sfx_attack":
                sfxObjectCreation(sfx_attack);
                break;
            case "sfx_run":
                sfxObjectCreation(sfx_run);
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
                musicObjectCreation(music_start);
                break;
            default:
                break;
        }
    }
    void musicObjectCreation(AudioClip clip){
        GameObject newObject = Instantiate(soundObject, transform);
        newObject.GetComponent<AudioSource>().clip = clip;
        newObject.GetComponent<AudioSource>().loop = true;
        newObject.GetComponent<AudioSource>().Play();
    }
}
