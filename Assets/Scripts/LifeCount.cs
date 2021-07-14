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
    public Vector3 respawnPosition;
    public SpriteRenderer spriteRenderer;

    public float noDamageTimeFrame = 1;
    public float lastDamageTime = -100;
    bool killingPlayer=false;
    

    void FixedUpdate(){
        if((Time.time - lastDamageTime)>damageBlinkTime){
            spriteRenderer.color = new Color(255,255,255);
        }
        if(gameObject.GetComponent<Transform>().position.y<-10){
            livesRemaining = 0;
            LoseLife();
        }
    }

    public void LoseLife(){
        if((lastDamageTime+noDamageTimeFrame)>=Time.time) return;
        //if(!killingPlayer) AudioManager.instance.playSFX("sfx_looselife");
        lastDamageTime = Time.time;
        spriteRenderer.color = damageColor;
        if(livesRemaining>0){
            livesRemaining--;
            lives[livesRemaining].enabled = false;
        }
        if(livesRemaining==0 && !killingPlayer){
            foreach(Image live in lives){
                live.enabled = false;
            }
            killingPlayer=true;
            StartCoroutine(KillPlayer());
            
        }

    }
    IEnumerator KillPlayer(){
        gameObject.GetComponent<PlayerMovement>().setCanMove(false);
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Transform>().position = respawnPosition;
        gameObject.GetComponent<PlayerMovement>().setCanMove(true);
        livesRemaining=3;
        foreach(Image live in lives){
                live.enabled = true;
        }
        FindObjectOfType<InteractSystem>().resetInteractables();
        GameController.instance.playerDeaths++;
        killingPlayer = false;        
    }
}
