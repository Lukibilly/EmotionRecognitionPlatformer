using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    public Transform detectionPoint;
    private const float detectionRadius = 0.2f;
    public LayerMask detectionLayer;
    Collider2D detectedObject;

    void Update()
    {
        if(DetectObject()){
            detectedObject.GetComponent<Interactable>().Interact();
        }
    }

    bool DetectObject(){
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius,detectionLayer);

        if(obj==null) return false;

        detectedObject = obj;
        return true;
    }
    public void doInteraction(GameObject interactable,Interactable script){
        Debug.Log("I got picked up");
        if(script.type==Interactable.InteractionType.Speed) speedUpPlayer();
        Destroy(interactable);
    }

    void speedUpPlayer(){
        gameObject.GetComponent<PlayerMovement>().runSpeed+=10;
    }
}
