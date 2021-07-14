using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    public Transform detectionPoint;
    private const float detectionRadius = 0.2f;
    public LayerMask detectionLayer;
    Collider2D detectedObject;
    public float pillSpeedBonus = 5;
    public float pillSpeedtime = 5;
    public bool spedUp = false;
    float lastSpeed;
    List<GameObject> usedInteractables = new List<GameObject>();

    void Update()
    {
        if(spedUp){
            if((Time.time-lastSpeed)>pillSpeedtime){
                spedUp = false;
            }
        }
    }

    public void doInteraction(GameObject interactable,Interactable script){
        //Debug.Log("I got picked up");
        if(script.type==Interactable.InteractionType.Speed) speedUpPlayer();
        interactable.transform.parent.gameObject.SetActive(false);
        usedInteractables.Add(interactable.transform.parent.gameObject);
    }

    void speedUpPlayer(){
        lastSpeed = Time.time;
        spedUp = true;
    }
    public void resetInteractables(){
        foreach(GameObject interactable in usedInteractables){
            interactable.SetActive(true);
        }
        usedInteractables.Clear();
    }
}
