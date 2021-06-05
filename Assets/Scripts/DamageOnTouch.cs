using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DamageOnTouch : MonoBehaviour
{
    public float noDamageTimeFrame = 1;
    float lastDamageTime = -2;
    private void Reset(){
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision){

        if(collision.tag == "Player" && ((lastDamageTime+noDamageTimeFrame)<Time.time)){
            collision.gameObject.GetComponent<LifeCount>().LoseLife();
            lastDamageTime = Time.time;
        }
    }
}
