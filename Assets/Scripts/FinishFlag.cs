using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFlag : MonoBehaviour
{
    private GameObject player;
    public float maxDistance = 0;
    void Awake(){
        player = GameObject.Find("Player");
    }
    void FixedUpdate(){
        if(player.transform.position.x > maxDistance) maxDistance = player.transform.position.x;
    }
    private void OnTriggerStay2D(Collider2D collider){
        GameController.instance.finishGame(true);
    }
}
