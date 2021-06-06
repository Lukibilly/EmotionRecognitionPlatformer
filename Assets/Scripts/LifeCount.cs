using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    public Image[] lives;
    public int livesRemaining;
    public Color damageColor;    
    public float damageBlinkTime;
    public SpriteRenderer spriteRenderer;

    public float noDamageTimeFrame = 1;
    public float lastDamageTime = -100;
    

    void FixedUpdate(){
        if((Time.time - lastDamageTime)>damageBlinkTime){
            spriteRenderer.color = new Color(255,255,255);
        }
    }

    public void LoseLife(){
        lastDamageTime = Time.time;
        spriteRenderer.color = damageColor;
        if(livesRemaining>0){
            livesRemaining--;
            lives[livesRemaining].enabled = false;
        }
        if(livesRemaining==0){
            foreach(Image live in lives){
                live.enabled = false;
            }
        }

    }
}
