using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSound : MonoBehaviour
{
    AudioSource source;
    private float fadeSpeed = 5;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(source.volume > 1) source.volume = Mathf.Lerp(source.volume,0,fadeSpeed*Time.deltaTime);
        else{
            AudioManager.instance.startMusic();
            Destroy(gameObject);
        }
    }
}
