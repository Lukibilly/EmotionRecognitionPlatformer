using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    public Image[] lives;
    public int livesRemaining;

    public void LoseLife(){
        if(livesRemaining>0){
            livesRemaining--;
            lives[livesRemaining].enabled = false;
        }
    }
}
